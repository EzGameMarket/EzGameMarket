using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC.Views.Account
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Regisztr�l�s";
        }
    }
}
