using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Models
{
    public class CloudGamingProvidersAndGames
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public int CloudGamingProviderID { get; set; }
        [Required]
        public CloudGamingProvider Provider { get; set; }
        [Required]
        public int CloudGamingSupportedID { get; set; }
        [Required]
        public CloudGamingSupported Game { get; set; }
    }
}
