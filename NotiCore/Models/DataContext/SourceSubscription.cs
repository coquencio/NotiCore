﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class SourceSubscription
    {
        [Key]
        public int SourceSubscriptionId { get; set; }
        [Required]
        public int SubcriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        [Required]
        public int SourceId { get; set; }
        public Source Source { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
