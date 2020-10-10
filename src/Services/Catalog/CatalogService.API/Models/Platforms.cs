using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.Models
{
    public class Platform
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}