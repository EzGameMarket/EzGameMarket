using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Exceptions.ImageSize.Model;
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
    public class CatalogImageRepository : ICatalogImageRepository
    {
        private CatalogImagesDbContext _dbContext;
        private IImageSizeRepository _imageSizeRepository;
        private IImageTypeRepository _imageTypeRepository;

        public CatalogImageRepository(CatalogImagesDbContext dbContext, IImageSizeRepository imageSizeRepository, IImageTypeRepository imageTypeRepository)
        {
            _dbContext = dbContext;
            _imageSizeRepository = imageSizeRepository;
            _imageTypeRepository = imageTypeRepository;
        }

        public async Task Add(CatalogItemImageModel model)
        {
            await ValidateModelForAdd(model);

            await _dbContext.Images.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelForAdd(CatalogItemImageModel model)
        {
            if (await AnyWithID(model.ID.GetValueOrDefault()) == true)
            {
                throw new CImageAlreadyExistWithIDException() { ID = model.ID.GetValueOrDefault() };
            }

            if (await AnyWithUrl(model.ImageUri) == true)
            {
                throw new CImageAlreadyExistWithUrlException() { ProductUri = model.ImageUri };
            }
        }

        public async Task Delete(int id)
        {
            var img = await GetByID(id);

            if (img == default)
            {
                throw new CImageNotFoundException() { ID = id };
            }

            _dbContext.Images.Remove(img);
            await _dbContext.SaveChangesAsync();
        }

        public Task<CatalogItemImageModel> GetByID(int id) => _dbContext.Images.Include(i=> i.Type).Include(i=> i.Size).FirstOrDefaultAsync(i=> i.ID == id);

        public Task<bool> AnyWithID(int id) => _dbContext.Images.AnyAsync(i => i.ID == id);

        public Task<bool> AnyWithUrl(string url) => _dbContext.Images.AnyAsync(i=> i.ImageUri == url);

        public async Task Modify(int id, CatalogItemImageModel model)
        {
            await ValidateModelForModify(model);

            if (id != model.ID)
            {
                throw new ArgumentException($"Az {nameof(id)} paraméter és a {nameof(model.ID)} paraméter nem egyenlő");
            }

            _dbContext.Entry(model).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        private async Task ValidateModelForModify(CatalogItemImageModel model)
        {
            var id = model.ID.GetValueOrDefault();

            if (model.Size != default)
            {
                throw new CImageSizeNotAllowedUpdateException() { ImageID = id, Model = model.Size };
            }

            if (model.Type != default)
            {
                throw new CImageTypeNotAllowedUpdateException() { ImageID = id, Model = model.Type };
            }

            if (await AnyWithID(id) == false)
            {
                throw new CImageNotFoundException() { ID = id };
            }
        }

        public async Task ModifySize(int id, int sizeID)
        {
            var img = await GetByID(id);

            if (img == default)
            {
                throw new CImageNotFoundException() { ID = id };
            }

            if (await _imageSizeRepository.AnyWithID(sizeID) == false)
            {
                throw new ImageSizeNotFoundByIDException() { ID = id };
            }

            img.SizeID = sizeID;

            await _dbContext.SaveChangesAsync();
        }

        public async Task ModifyType(int id, int typeID)
        {
            var img = await GetByID(id);

            if (img == default)
            {
                throw new CImageNotFoundException() { ID = id };
            }

            if (await _imageTypeRepository.AnyWithID(typeID) == false)
            {
                throw new ImageTypeNotFoundByIDException() { ID = id };
            }

            img.TypeID = typeID;

            await _dbContext.SaveChangesAsync();
        }
    }
}
