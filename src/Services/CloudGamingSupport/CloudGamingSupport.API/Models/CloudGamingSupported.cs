using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Models
{
    public class CloudGamingSupported
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int? ID { get; set; }
        [Required]
        public string ProductID { get; set; }

        [Required]
        public List<CloudGamingProvidersAndGames> Providers { get; set; }
    }
}
