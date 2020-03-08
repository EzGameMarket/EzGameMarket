using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CatalogService.API.Models
{
    public class ProductImage
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        public string ProductID { get; set; }
        public Product Product { get; set; }
        [Required]
        public string Url { get; set; }

        [Required]
        public int SizeID { get; set; }

        public ImgSize Size { get; set; }
    }
}