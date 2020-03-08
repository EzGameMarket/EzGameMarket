using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.Models
{
    public class ImgSize
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        public string Size { get; set; }
    }
}