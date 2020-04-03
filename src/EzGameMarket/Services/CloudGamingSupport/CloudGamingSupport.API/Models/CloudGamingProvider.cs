using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudGamingSupport.API.Models
{
    public class CloudGamingProvider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string Url { get; set; }
        [Url]
        public string SearchURl { get; set; }

        [Required]
        public List<CloudGamingProvidersAndGames> SupportedGames { get; set; }
    }
}