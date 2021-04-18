using Microsoft.Extensions.Logging;
using NotiCore.API.Infraestructure.Common;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.MachineLearning.PredictArticleTopic;
using NotiCore.API.Models.ScrappedArticles;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class ArticleService : IArticleService
    {
        private readonly DataContext _context;
        private readonly IScraperService _scraperService;
        private readonly IMLService _mLService;
        private readonly ILogger<ArticleService> _logger;
        public ArticleService(DataContext context, IScraperService scraperService, IMLService mLService, ILogger<ArticleService> logger)
        {
            _context = context;
            _scraperService = scraperService;
            _mLService = mLService;
            _logger = logger;
        }
        public async Task SaveArticlesFromSourceAsync(Source source)
        {
            var scrappedArticles = _scraperService.ExtractNewsFromSource(source.Url);
            if (scrappedArticles.Any())
            {
                var listToAdd = new List<Article>();
                var tasks = scrappedArticles.Select(async article => {
                    var toAdd = ResolveArticleEntity(article);
                    toAdd.SourceId = source.SourceId;

                    toAdd.TopicId = TryGetTopicFromUrl(source.Url, toAdd.Url);
                    if (!string.IsNullOrEmpty(toAdd.Url)
                    && _context.Articles.Where(a => a.Url.Equals(toAdd.Url) && toAdd.SourceId == a.SourceId).Count() == 0)
                    {
                        try
                        {
                            if (toAdd.TopicId == (int)Infraestructure.Common.Topic.Other)
                            {
                                var prediction = PredictTopic(toAdd.Summary);
                                if (prediction.Accuracy < Constants.MinimumTopicAccuracy)
                                {
                                    var text = await _scraperService.ExtractWordsFromUrlAsync(toAdd.Url);
                                    if (!string.IsNullOrEmpty(text))
                                    {
                                        var secondPrediction = PredictTopic(text);
                                        if (secondPrediction.Accuracy > Constants.MinimumTopicAccuracy)
                                        {
                                            toAdd.TopicId = secondPrediction.TopicId;
                                            toAdd.Accuracy = secondPrediction.Accuracy;
                                        }
                                    }
                                }
                                else
                                {
                                    toAdd.TopicId = prediction.TopicId;
                                    toAdd.Accuracy = prediction.Accuracy;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error ocurred when trying to pull data from {url} ", $"url: {toAdd.Url}");
                        }
                        listToAdd.Add(toAdd);
                    }
                });
                await Task.WhenAll(tasks);

                if (listToAdd.Any())
                {
                    _context.Articles.AddRange(listToAdd);
                    foreach (var entity in listToAdd)
                    {
                        entity.ScrapedDate = DateTime.Now;
                    }
                }
                source.LastScrapedDate = DateTime.Now;
                _context.Sources.Update(source);
                _context.SaveChanges();
            }
        }
        private int TryGetTopicFromUrl(string baseUrl, string fullUrl)
        {
            var url = fullUrl.Replace(baseUrl, "");
            var topic = _context.Topic.FirstOrDefault(t=> t.IsActive && url.ToLower().Contains(t.Description.ToLower()));
            if (topic != null)
                return topic.TopicId;
            return (int)Infraestructure.Common.Topic.Other;
        }
        private (int TopicId, float Accuracy) PredictTopic(string summary)
        {
            var input = new PredictArticleTopicInput()
            {
                Text = summary
            };
            var output = _mLService.PredictTopic(input);
            var topSocore = output.Score.Max();
            return (Convert.ToInt32(output.Prediction), topSocore);
        }
        private Article ResolveArticleEntity(ArticleBaseModel baseModel)
        {
            var date = new DateTime(); 
            DateTime.TryParse(baseModel.Published, out date);
            var article = new Article()
            {
                Title = baseModel.Title,
                Url = baseModel.Link,
                Authors = baseModel.Author,
                PublishedDate = date
            };
            ResolveSummary(article, baseModel);
            if (baseModel.MediaContent != null && baseModel.MediaContent.Any())
                article.ImageSource = baseModel.MediaContent.First().Url;

            return article;
        }
        private void ResolveSummary(Article article, ArticleBaseModel baseModel)
        {
            if (baseModel.Content != null && baseModel.Content.Any())
            {
                article.Summary = baseModel.Content.First().Value;
            }
            article.Summary = baseModel.Summary;
        }
    }
}
