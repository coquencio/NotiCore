using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Helpers
{
    public static class TemplateHelper
    {
        private static string _templateRoute = @"../Noticore/Infraestructure/EmailTemplates/NewsLetter/";
        public static string GetEmailEnrollmentTemplate(string name, string link)
        {
            var route = @"../Noticore/Infraestructure/EmailTemplates/EnrollTemplate/EnrollConfirmationTemplate.html";
            var template = File.ReadAllText(route);
            template = template.Replace("{year}", DateTime.Now.Year.ToString());
            template = template.Replace("{firstName}", name);
            template = template.Replace("{link}", link);
            return template;
        }
        public static string GetNewsLetterTemplate(string userNmae, ICollection<Article> articles, string deactivateUrl)
        {
            var template = "";
            var sources = new List<string>();
            foreach (var source in articles.Select(a => a.Source))
            {
                if (!sources.Contains(source.Name))
                    sources.Add(source.Name);
            }
            template += GetMainHeader(userNmae, articles.Count().ToString(), sources.Count.ToString(), deactivateUrl);
            foreach (var source in sources)
            {
                template += GetSourceHeaderTemplate(source);
                var includedArticles = articles.Where(a => a.Source.Name.Equals(source));
                var topics = new List<string>();
                foreach (var article in includedArticles)
                {
                    if (!topics.Contains(article.Topic.Description))
                        topics.Add(article.Topic.Description);
                }
                foreach (var topic in topics)
                {
                    var topicArticles = includedArticles.Where(a => a.Topic.Description.Equals(topic));
                    template += GetCategoryHeader(topic);
                    foreach (var article in topicArticles)
                    {
                        template += GetArticleTemplate(article.Title, article.Summary, article.Url);
                    }
                }
                
            }
            template += GetFooterTemplate();
            return template;
        }
        public static string GetArticleTemplate(string title, string content, string url)
        {
            content = content ?? "";
            content = content.FormatImages();
            if (content.Length > 400)
            {
                content = content.BuildSummaryClosingAllTags();
            }
            var template = File.ReadAllText(_templateRoute  + "ArticleTemplate.html");
            template = template.Replace("{Title}", title).Replace("{content}", content).Replace("{url}", url);
            return template;
        }
        public static string GetSourceHeaderTemplate(string source)
        {
            var template = File.ReadAllText(_templateRoute + "SourceHeaderTemplate.html");
            template = template.Replace("{source}", source);
            return template;
        }
        public static string GetFooterTemplate()
        {
            var template = File.ReadAllText(_templateRoute + "FooterTemplate.html");
            template = template.Replace("{year}", DateTime.Now.Year.ToString());
            return template;
        }
        public static string GetCategoryHeader(string category)
        {
            var template = File.ReadAllText(_templateRoute + "CatDividerTemplate.html");
            template = template.Replace("{Category}", category);
            return template;
        }
        public static string GetMainHeader(string firstName, string totalArticles, string totalSource, string deactivateUrl)
        {
            var template = File.ReadAllText(_templateRoute + "HeaderTemplate.html");
            template = template
                .Replace("{firstName}", firstName)
                .Replace("{totalArticles}", totalArticles)
                .Replace("{totalSources}", totalSource)
                .Replace("{deactivateLink}", deactivateUrl);
            return template;
        }
    }
}
