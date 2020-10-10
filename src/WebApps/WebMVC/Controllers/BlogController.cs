using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}