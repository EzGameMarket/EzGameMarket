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
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [Route("product/{productID}")]
        public async Task<ActionResult<List<Tag>>> GetTagsForProduct(string productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return BadRequest();
            }

            var tags = await _tagRepository.GetTagsForProduct(productID);

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
        [Route("{tagID}")]
        public async Task<ActionResult<Tag>> GetTag(int tagID)
        {
            if (tagID <= 0)
            {
                return BadRequest();
            }

            var tag = await _tagRepository.GetTag(tagID);

            if (tag != default)
            {
                return tag;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("products/{tagID}")]
        public async Task<ActionResult<List<string>>> GetProductsForTagID([FromRoute] int tagID)
        {
            if (tagID <= 0)
            {
                return BadRequest();
            }

            try
            {
                return await _tagRepository.GetProductsForTagID(tagID);
            }
            catch (TagNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("product/add")]
        public async Task<ActionResult> AddNewProductToTagAsync([FromBody] NewProductToTagViewModel model)
        {
            if (model.TagID <= 0 || string.IsNullOrEmpty(model.ProductID))
            {
                return BadRequest();
            }

            try
            {
                await _tagRepository.AddProductToTag(model);

                return Ok();
            }
            catch (TagNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] Tag newTag)
        {
            if (string.IsNullOrEmpty(newTag.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _tagRepository.AddNewTag(newTag);

                return Ok();
            }
            catch (TagAlreadyInDataBaseException)
            {
                return Conflict();
            }
        }

        [HttpPost]
        [Route("modify/{tagID}")]
        public async Task<ActionResult> Update([FromRoute] int tagID,[FromBody] Tag model)
        {
            if (tagID <= 0 || string.IsNullOrEmpty(model.Name) || ModelState.IsValid == false)
            {
                return BadRequest();
            }

            try
            {
                await _tagRepository.Modify(tagID,model);

                return Ok();
            }
            catch (TagNotFoundException)
            {
                return NotFound();
            }
        }
    }
}