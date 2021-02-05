using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class TopicSubscription
    {
        [Key]
        public int TopicSubscriptionId { get; set; }
        [Required]
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        [Required]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
