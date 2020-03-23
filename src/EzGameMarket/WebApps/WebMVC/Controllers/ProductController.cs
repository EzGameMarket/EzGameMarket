using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("https://localhost:32790/api/Products");
        }
    }
}