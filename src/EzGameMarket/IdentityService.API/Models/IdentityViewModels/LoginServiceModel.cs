using System.ComponentModel.DataAnnotations;

namespace IdentityService.API.Models.IdentityViewModels
{
    public class LoginServiceModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}