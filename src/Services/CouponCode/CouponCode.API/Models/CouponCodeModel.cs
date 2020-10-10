using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CouponCode.API.Models
{
    public class CouponCodeModel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public DiscountModel Discount { get; set; }
        [Required]
        public int DiscountID { get; set; }

        [Required]
        public bool IsLimitedForUsers { get; set; }

        public List<EligibleUserModel> Users { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
