using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Models
{
    public class CatalogItemImageModel
    {
        [NotMapped]
        public static int MaxFileLength = 2_097_152;

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        [Url]
        public string ImageUri { get; set; }

        [Required]
        public string ProductID { get; set; }

        [Required]
        public int TypeID { get; set; }
        public ImageTypeModel Type { get; set; }

        [Required]
        public int SizeID { get; set; }
        public ImageSizeModel Size { get; set; }
    }
}
