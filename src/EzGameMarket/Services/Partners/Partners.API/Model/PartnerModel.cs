using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.API.Model
{
    public class PartnerModel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<SocialMediaType> SocialMedia { get; set; }

        [Required]
        public IEnumerable<PartnerPlatformModel> Platforms { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public int Subscribers { get; set; }

        [Required]
        public DateTime PatnershipStarted { get; set; }
        [Required]
        public bool Active { get; set; }

        public string CouponCode { get; set; }

        public int Discount { get; set; }
    }
}
