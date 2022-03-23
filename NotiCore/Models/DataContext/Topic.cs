using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        [Required]
        [StringLength(20)]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
