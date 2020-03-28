using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return Redirect("https://localhost:32794/identity/account/login");
        }

        public IActionResult Register()
        {
            return Redirect("https://localhost:32794/identity/account/register");
        }
    }
}