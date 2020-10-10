using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Users.PaymentMethods.API.Models
{
    public class UserPaymentMethodsModel
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        public string UserID { get; set; }

        public IEnumerable<CreditCardModel> CreditCards { get; set; }
        public IEnumerable<BarionModel> Barions { get; set; }
    }
}
