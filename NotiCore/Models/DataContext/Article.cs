using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

        [Required]
        public string Url { get; set; }

        public string Authors { get; set; }

        public int? TopicId { get; set; }
        public Topic Topic { get; set; }

        public int SourceId { get; set; }
        public Source Source { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime ScrapedDate { get; set; }
        public string ImageSource { get; set; }
        public bool Sent { get; set; }

    }
}
