using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC.Views.Account
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Regisztrálás";
        }
    }
}