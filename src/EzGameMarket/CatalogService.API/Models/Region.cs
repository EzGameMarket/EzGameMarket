using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.Models
{
    public class Region
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}