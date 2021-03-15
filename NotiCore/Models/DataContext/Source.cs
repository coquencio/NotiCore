using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class Source
    {
        [Key]
        public int SourceId { get; set; }

        [Required]
        public string Url { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public ICollection<Article> Articles { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime LastScrapedDate { get; set; }
    }
}
