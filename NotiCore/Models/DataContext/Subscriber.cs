using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Models.DataContext
{
    public class Subscriber
    {
        [Key]
        public int SubscriberId { get; set; }
        [Required]
        [StringLength(60)]
        public string Email { get; set; }
        [Required]
        public bool HasAuthorized { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
    }
}
