using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC.Views.Account
{
    public class LoginModel : PageModel
    {
        public class InputModel
        {
        }

        public void OnGet()
        {
            ViewData["Title"] = "Bejelentkezés";
        }

        public void OnPost()
        {
        }
    }
}