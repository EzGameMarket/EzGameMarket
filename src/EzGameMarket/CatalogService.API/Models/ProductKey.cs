using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Models
{
    /// <summary>
    /// Itt lehet tárolni azokat a kulcsokat amiket nem a a CodesWholeSaletől vásároltam
    /// </summary>
    public class ProductKey
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string ProductID { get; set; }

        [Required]
        public bool Used { get; set; }

        [Required]
        public DateTime UsedDate { get; set; }

        [Required]
        public string BuyerID { get; set; }
    }
}
