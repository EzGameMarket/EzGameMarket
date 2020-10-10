using Categories.API.Data;
using Categories.API.Models;
using Categories.API.Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Categories.API.Exceptions;
using System.Threading.Tasks;
using Categories.API.ViewModels;

namespace Categories.API.Services.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        private CategoryDbContext _dbContext;

        public TagRepository(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewTag(Tag newTag)
        {
            if (newTag.ID.HasValue)
            {
                var tag = await GetTag(newTag.ID.Value);

                if (tag != default)
                {
                    throw new TagAlreadyInDataBaseException() { TagID = newTag.ID.Value, TagName = newTag.Name };
                }
            }

            newTag.ID = default;

            await _dbContext.Tags.AddAsync(newTag);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductToTag(NewProductToTagViewModel model)
        {
            var tag = await GetTag(model.TagID);

            if (tag == default)
            {
                throw new TagNotFoundException() { TagID = model.TagID };
            }

            if (tag.Products == default)
            {
                tag.Products = new List<TagsAndProductsLink>();
            }

            tag.Products.Add(new TagsAndProductsLink() { ProductID = model.ProductID });

            await _dbContext.SaveChangesAsync();
        }

        public async Task Modify(int tagID, Tag model)
        {
            var tag = await GetTag(tagID);

            if (tag == default)
            {
                throw new TagNotFoundException() { TagID = tagID };
            }

            _dbContext.Entry(tag).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }

        public Task<Tag> GetTag(int tagID) => _dbContext.Tags.Include(t=> t.Products).FirstOrDefaultAsync(t=> t.ID == tagID);

        public Task<List<Tag>> GetTagsForProduct(string productID) => _dbContext.Tags.Include(t => t.Products).Where(t=> t.Products.Any(p=> p.ProductID == productID)).ToListAsync();

        public async Task<List<string>> GetProductsForTagID(int tagID)
        {
            var tag = await GetTag(tagID);

            if (tag != default)
            {
                return tag.Products.Select(p=> p.ProductID).ToList();
            }
            else
            {
                throw new TagNotFoundException() { TagID = tagID };
            }
        }
    }
}
