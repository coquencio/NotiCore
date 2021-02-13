using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.ScrappedArticles
{
    public class RTExample
    {
        public string Author { get; set; }

        public record AuthorDetails
        {
            public string Name { get; set; }
        }
        public AuthorDetails AuthorDetail { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public record ContentDetail
        {
            public string Base { get; set; }
            public string Language { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
        }
        public IEnumerable<ContentDetail> Content { get; set; }
        public string Published { get; set; }
    }
}
