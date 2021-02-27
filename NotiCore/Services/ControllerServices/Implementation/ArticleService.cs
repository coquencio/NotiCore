using NotiCore.API.Models.DataContext;
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
        public ArticleService(DataContext context, IScraperService scraperService)
        {
            _context = context;
            _scraperService = scraperService;
        }
        public void SaveArticlesFromSource(Source source)
        {
            var scrappedArticles = _scraperService.ExtractNewsFromSource(source.Url);
            foreach (var article in scrappedArticles)
            {
                var toAdd = ResolveArticleEntity(article);
                toAdd.SourceId = source.SourceId;
                _context.Add(toAdd);
            }
            _context.SaveChanges();
        }
        private Article ResolveArticleEntity(ArticleBaseModel baseModel)
        {
            var article = new Article()
            {
                Title = baseModel.Title,
                Url = baseModel.Link,
                Authors = baseModel.Author,
                PublishedDate = Convert.ToDateTime(baseModel.Published),
                ScrapedDate = DateTime.Now
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
