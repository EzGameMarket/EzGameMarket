using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingService.API.Models
{
    public class NewsletterMessage
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime? Sended { get; set; }
    }
}
