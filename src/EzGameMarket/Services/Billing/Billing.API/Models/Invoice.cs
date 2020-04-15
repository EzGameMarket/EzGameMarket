using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Models
{
    public class Invoice
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public int? BillFileID { get; set; }
        public InvoiceFile BillFile { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string CompanyName { get; set; }
        
        public string VATNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostCode { get; set; }

        [Required]
        public IEnumerable<InvoiceItem> Items { get; set; }

        [Required]
        public int Total { get; set; }

        [Required]
        public DateTime FullfiledDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
