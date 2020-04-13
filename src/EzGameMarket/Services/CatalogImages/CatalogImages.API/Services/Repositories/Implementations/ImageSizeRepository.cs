using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageSize.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Implementations
{
    public class ImageSizeRepository : IImageSizeRepository
    {
        private CatalogImagesDbContext _dbContext;

        public ImageSizeRepository(CatalogImagesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ImageSizeModel model)
        {
            await ValidateModelToUpload(model);

            await _dbContext.ImageSizes.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelToUpload(ImageSizeModel model)
        {
            var id = model.ID.GetValueOrDefault();

            if (await AnyWithID(id) == true)
            {
                throw new ImageSizeAlreadyExistWithIDException() { ID = id };
            }

            if (await IsThereAnySizeWithThisDimensions(model.Width,model.Height) == true)
            {
                throw new ImageSizeAlreadyExistWithDimensionException() { Width = model.Width, Height = model.Height };
            }

            if (await AnyWithName(model.Name) == true)
            {
                throw new ImageSizeAlreadyExistWithNameException() { Name = model.Name };
            }
        }

        private Task<bool> IsThereAnySizeWithThisDimensions(int width, int height) => 
            _dbContext.ImageSizes.AnyAsync(imgSize => imgSize.Width == width && imgSize.Height == height);

        public Task<bool> AnyWithID(int id) => _dbContext.ImageSizes.AnyAsync(imgSize => imgSize.ID.GetValueOrDefault() == id);
        public Task<bool> AnyWithName(string name) => _dbContext.ImageSizes.AnyAsync(imgSize => imgSize.Name == name);

        public async Task Delete(int id)
        {
            var imgSize = await GetByID(id);

            if (imgSize == default)
            {
                throw new ImageSizeNotFoundByIDException() { ID = id };
            }

            _dbContext.ImageSizes.Remove(imgSize);

            await _dbContext.SaveChangesAsync();
        }

        public Task<List<ImageSizeModel>> GetAllSizes() => _dbContext.ImageSizes.ToListAsync();

        public Task<ImageSizeModel> GetByID(int id) => _dbContext.ImageSizes.FirstOrDefaultAsync(imgSize => imgSize.ID.GetValueOrDefault() == id);
        public Task<ImageSizeModel> GetByIDWithImages(int id) => _dbContext.ImageSizes.Include(i=> i.Images).FirstOrDefaultAsync(imgSize => imgSize.ID.GetValueOrDefault() == id);

        public async Task Modify(int id, ImageSizeModel model)
        {
            await ValidateModelForUpdate(id, model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelForUpdate(int id,ImageSizeModel model)
        {
            if (id != model.ID.GetValueOrDefault())
            {
                throw new ArgumentException($"Az {nameof(id)} paraméter és a {nameof(model.ID)} paraméter nem egyenlő");
            }

            if (await AnyWithID(id) == false)
            {
                throw new ImageSizeNotFoundByIDException() { ID = id };
            }
        }
    }
}
