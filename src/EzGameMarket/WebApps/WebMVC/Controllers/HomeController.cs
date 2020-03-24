using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Context()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult TermsOfUse()
        {
            return View();
        }

        public IActionResult ShipPolicy()
        {
            return View("ShippingPolicy");
        }

        public IActionResult ReturnsPolicy()
        {
            return View();
        }

        public IActionResult CookieUse()
        {
            return View();
        }

    }
}