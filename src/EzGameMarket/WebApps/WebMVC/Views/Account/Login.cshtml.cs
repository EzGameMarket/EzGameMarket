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
            ViewData["Title"] = "Bejelentkez�s";
        }

        public void OnPost()
        {
        }
    }
}