using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CartService.API.Models.ViewModels
{
    public class CheckoutModel
    {
        [Required]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Address1 { get; set; }
        [DataType(DataType.Text)]
        public string Address2 { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }
        [DataType(DataType.Text)]
        public string VATNumber { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string BuyerID { get; set; }
        [Required]
        public List<CartItemCheckoutModel> Items { get; set; }
    }
}