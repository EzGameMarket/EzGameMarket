using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Categories.API.Exceptions;
using Categories.API.Models;
using Categories.API.Services.Repositories.Abstractions;
using Categories.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Categories.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("product/{productID}")]
        public async Task<ActionResult<List<Category>>> GetCategoriesForProduct(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var tags = await _categoryRepository.GetCategoriesForProduct(productID);

            if (tags != default && tags.Count > 0)
            {
                return tags;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{categoryID}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryID)
        {
            if (categoryID <= 0)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetCategory(categoryID);

            if (category != default)
            {
                return category;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("products/{categoryID}")]
        public async Task<ActionResult<List<string>>> GetProductsForCategoryID([FromRoute] int categoryID)
        {
            if (categoryID <= 0)
            {
                return BadRequest();
            }

            try
            {
                return await _categoryRepository.GetProductsForCategoryID(categoryID);
            }
            catch (CategoryNotFoundInDataBaseException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("product/add")]
        public async Task<ActionResult> AddNewProductToCategoryAsync([FromBody] NewProductToCategoryViewModel model)
        {
            if (model.CategoryID <= 0 || string.IsNullOrEmpty(model.ProductID))
            {
                return BadRequest();
            }

            try
            {
                await _categoryRepository.AddProductToCategories(model);

                return Ok();
            }
            catch (CategoryNotFoundInDataBaseException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] Category newCategory)
        {
            if (string.IsNullOrEmpty(newCategory.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _categoryRepository.AddNewCategory(newCategory);

                return Ok();
            }
            catch (CategoryAlreadyInDataBaseException)
            {
                return Conflict();
            }
        }

        [HttpPost]
        [Route("modify/{categoryID}")]
        public async Task<ActionResult> Update([FromRoute] int tagID, [FromBody] Category model)
        {
            if (tagID <= 0 || string.IsNullOrEmpty(model.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _categoryRepository.Modify(tagID, model);

                return Ok();
            }
            catch (CategoryNotFoundInDataBaseException)
            {
                return NotFound();
            }
        }
    }
}