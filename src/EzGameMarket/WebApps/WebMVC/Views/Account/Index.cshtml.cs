using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC.Views.Account
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Felhaszn�l� kezel�s";
        }
    }
}