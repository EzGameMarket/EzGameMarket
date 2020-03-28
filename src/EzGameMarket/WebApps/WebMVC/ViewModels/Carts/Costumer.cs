using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels.Carts
{
    public class Costumer
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefonszámod:")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email címed:")]
        public string Email { get; set; }
    }
}