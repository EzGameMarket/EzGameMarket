using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Models
{
    public class SubscribedMember
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public DateTime SubscribedDate { get; set; }
        [Required]
        public DateTime UnSubscribedDate { get; set; }
        public bool Active { get; set; }
    }
}
