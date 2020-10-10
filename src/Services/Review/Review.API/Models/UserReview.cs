using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Review.API.Models
{
    public class UserReview
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public string ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ReviewText { get; set; }
    }
}
