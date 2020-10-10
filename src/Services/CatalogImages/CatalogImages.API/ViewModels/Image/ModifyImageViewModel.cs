using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.ViewModels.Image
{
    public class ModifyImageViewModel
    {
        public IFormFile NewImage { get; set; }

        public string ProductID { get; set; }
    }
}
