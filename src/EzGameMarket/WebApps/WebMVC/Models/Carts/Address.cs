using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models.Carts
{
    public class Address
    {
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Irányítószám:")]
        public string PostCode { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Város:")]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Ország:")]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Cím:")]
        public string Address1 { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Cím2:")]
        public string Address2 { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Keresztneved:")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Vezetékneved:")]
        public string LastName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Cégnév:")]
        public string CompanyName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Adószám:")]
        public string VATNumber { get; set; }
    }
}