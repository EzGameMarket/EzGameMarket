using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.ImageType.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Implementations
{
    public class ImageTypeRepository : IImageTypeRepository
    {
        private CatalogImagesDbContext _dbContext;

        public ImageTypeRepository(CatalogImagesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ImageTypeModel model)
        {
            await ValidateModelForUpload(model);

            await _dbContext.ImageTypes.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelForUpload(ImageTypeModel model)
        {
            var id = model.ID.GetValueOrDefault();

            if (await AnyWithID(id) == true)
            {
                throw new ImageTypeAlreadyExistsWithIDException() { ID = id };
            }

            if (await AnyWithName(model.Name) == true)
            {
                throw new ImageTypeAlreadyExistsWithNameException() { Name = model.Name };
            }
        }

        public Task<bool> AnyWithID(int id) => _dbContext.ImageTypes.AnyAsync(imgType => imgType.ID.GetValueOrDefault() == id);

        public Task<bool> AnyWithName(string name) => _dbContext.ImageTypes.AnyAsync(imgType => imgType.Name == name);

        public async Task Delete(int id)
        {
            var img = await GetByID(id);

            if (img == default)
            {
                throw new ImageTypeNotFoundByIDException() { ID = id };
            }

            _dbContext.ImageTypes.Remove(img);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<ImageTypeModel>> GetAllTypes() => _dbContext.ImageTypes.ToListAsync();

        public Task<ImageTypeModel> GetByID(int id) => _dbContext.ImageTypes.FirstOrDefaultAsync(imgType => imgType.ID == id);

        public Task<ImageTypeModel> GetByIDWithImages(int id) => _dbContext.ImageTypes.Include(i => i.Images).FirstOrDefaultAsync(imgType => imgType.ID == id);

        public async Task Modify(int id, ImageTypeModel model)
        {
            await ValidateModelForModify(id, model);

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelForModify(int id, ImageTypeModel model)
        {
            if (id != model.ID.GetValueOrDefault())
            {
                throw new ArgumentException($"Az {nameof(id)} paraméter és a {nameof(model.ID)} paraméter nem egyenlő");
            }

            if (await AnyWithID(id) == false)
            {
                throw new ImageTypeNotFoundByIDException() { ID = id };
            }
        }
    }
}
