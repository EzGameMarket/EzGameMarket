using System.ComponentModel.DataAnnotations;

namespace CatalogService.API.ViewModels
{
    public class PicQueryViewModel
    {
        [Required]
        public string ItemID { get; set; }

        [Required]
        public string Size { get; set; }
    }
}