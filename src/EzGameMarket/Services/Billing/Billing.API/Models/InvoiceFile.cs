using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Models
{
    public class InvoiceFile
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public int? BillID { get; set; }

        public Invoice Bill { get; set; }

        [Required]
        [Url]
        public string FileUri { get; set; }
    }
}
