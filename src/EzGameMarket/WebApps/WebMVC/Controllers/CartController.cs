using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}