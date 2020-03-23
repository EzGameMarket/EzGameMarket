using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("https://localhost:32794/identity/account/login");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}