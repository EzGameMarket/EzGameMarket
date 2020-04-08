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
            var baseURL = "https://localhost:7001/identity/account/login";

            return Redirect(baseURL);
        }

        public IActionResult Register()
        {
            return Redirect("https://localhost:7001/identity/account/register");
        }

        public IActionResult SubmitLogin()
        {


            return RedirectToAction("Index","Home");
        }

        public IActionResult SubmitRegister()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}