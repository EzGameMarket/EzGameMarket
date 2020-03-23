using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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