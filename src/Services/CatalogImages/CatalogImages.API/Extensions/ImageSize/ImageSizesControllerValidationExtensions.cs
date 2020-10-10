using CatalogImages.API.Controllers;
using CatalogImages.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Extensions.ImageSize
{
    public static class ImageSizesControllerValidationExtensions
    {
        public static ActionResult ValidateModelForUpload(this ImageSizesController controller, ImageSizeModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return controller.BadRequest("A név nem lehet null");
            }

            if (model.Width < ImageSizeModel.MinWidth)
            {
                return controller.BadRequest($"A szélleség nem lehet kisebb mint {ImageSizeModel.MinWidth}");
            }

            if (model.Height < ImageSizeModel.MinHeight)
            {
                return controller.BadRequest($"A magasság nem lehet kisebb mint {ImageSizeModel.MinHeight}");
            }

            if (model.Width > ImageSizeModel.MaxWidth)
            {
                return controller.BadRequest($"A szélleség nem lehet kisebb mint {ImageSizeModel.MaxWidth}");
            }

            if (model.Height > ImageSizeModel.MaxHeight)
            {
                return controller.BadRequest($"A magasság nem lehet nagyobb mint {ImageSizeModel.MaxHeight}");
            }

            return default;
        }

        public static async Task<ActionResult> ValidateModifyData(this ImageSizesController controller, int? id, ImageSizeModel model)
        {
            if (id != model.ID)
            {
                return controller.BadRequest($"A megadott ID és a Modelnek az IDja nem egyenlő");
            }

            if (controller.ModelState.IsValid == false || id == default || id <= 0)
            {
                return controller.BadRequest();
            }

            if (await controller.ImageSizeExists(id) == false)
            {
                return controller.NotFound();
            }

            var validateResult = controller.ValidateModelForUpload(model);

            if (validateResult != default)
            {
                return validateResult;
            }

            return default;
        }

        public static async Task<ActionResult> ValidateUploadData(this ImageSizesController controller, ImageSizeModel model)
        {
            if (controller.ModelState.IsValid == false)
            {
                return controller.BadRequest();
            }

            var validateResult = controller.ValidateModelForUpload(model);

            if (validateResult != default)
            {
                return validateResult;
            }

            if (await controller.ImageSizeExists(model.ID) == true)
            {
                return controller.Conflict();
            }

            return default;
        }
    }
}
