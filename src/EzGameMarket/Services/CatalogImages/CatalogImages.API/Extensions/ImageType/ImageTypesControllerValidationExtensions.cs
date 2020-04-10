using CatalogImages.API.Controllers;
using CatalogImages.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Extensions.ImageType
{
    public static class ImageTypesControllerValidationExtensions
    {
        public static async Task<ActionResult> ValidateModifyData(this ImageTypesController controller, int? id, ImageTypeModel model)
        {
            if (id <= 0)
            {
                return controller.BadRequest("Az ID nem lehet kisebb mint 1");
            }

            if (id != model.ID)
            {
                return controller.BadRequest($"A megadott ID és a Modelnek az IDja nem egyenlő");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return controller.BadRequest("A név nem lehet null");
            }

            if (controller.ModelState.IsValid == false || id == default || id <= 0)
            {
                return controller.BadRequest();
            }

            if (await controller.ImageTypeExists(id) == false)
            {
                return controller.NotFound();
            }

            return default;
        }

        public static async Task<ActionResult> ValidateUploadData(this ImageTypesController controller, ImageTypeModel model)
        {
            if (controller.ModelState.IsValid == false)
            {
                return controller.BadRequest();
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                return controller.BadRequest("A név nem lehet null");
            }

            if (await controller.ImageTypeExists(model.ID) == true)
            {
                return controller.Conflict($"Már létezik képtípus {model.ID} aznosítóval");
            }

            if (await controller.ImageTypeWithNameExists(model.Name) == true)
            {
                return controller.NotFound($"Már létezik {model.Name} képtípus");
            }

            return default;
        }
    }
}
