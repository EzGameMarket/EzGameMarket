using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Models
{
    public class Campaign
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public int Title { get; set; }
        
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string CampaignImage { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

        public string CouponCode { get; set; }
    }
}
