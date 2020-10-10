using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Models
{
    public class OwnCompanyDataModel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        [Required]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(14)]
        public string VATNumber { get; set; }

        [Required]
        [MaxLength(9)]
        public string EUVATNumber { get; set; }

        [Required]
        public int BankAccountID { get; set; }
        [Required]
        public int InvoiceBlockID { get; set; }
    }
}
