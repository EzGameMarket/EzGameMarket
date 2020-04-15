using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Models
{
    public class InvoiceItem
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string ProductID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int NetPrice { get; set; }

        [Required]
        public int BruttoPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
