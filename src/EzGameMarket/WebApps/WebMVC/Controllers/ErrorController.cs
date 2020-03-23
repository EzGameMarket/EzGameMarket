using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            var uri = "~/Home/Index";

            switch (statusCode)
            {
                case 401:
                    uri = "UnAuthorized";
                    break;

                case 403:
                    uri = "ForBidden";
                    break;

                case 404:
                    uri = "NotFound";
                    break;
            }

            return View(uri, model);
        }
    }
}