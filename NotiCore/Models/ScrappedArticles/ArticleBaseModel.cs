using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.ScrappedArticles
{
    public class ArticleBaseModel
    {
        public string Author { get; set; }

        public record AuthorDetails
        {
            public string Name { get; set; }
        }
        public AuthorDetails AuthorDetail { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public IEnumerable<ContentDetail> Content { get; set; }
        public ContentDetail SummaryDetail { get; set; }
        public string Summary { get; set; }
        public string Published { get; set; }
        public IEnumerable<Media> MediaContent{ get; set; }
        public record Media
        {
            public double Height { get; set; }
            public string Medium { get; set; }
            public string Type { get; set; }
            public string Url { get; set; }
        }
        public record ContentDetail
        {
            public string Base { get; set; }
            public string Language { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
}
