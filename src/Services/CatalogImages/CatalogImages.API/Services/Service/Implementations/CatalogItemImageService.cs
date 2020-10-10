using CatalogImages.API.Data;
using CatalogImages.API.Exceptions.Images.Model;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using CatalogImages.API.Services.Service.Abstractions;
using CatalogImages.API.ViewModels.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Utilities.CloudStorage.Shared.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Implementations
{
    public class CatalogItemImageService : ICatalogItemImageService
    {
        private CatalogImagesDbContext _dbContext;
        private ICatalogImageRepository _catalogImageRepository;
        private IStorageService _storageService;

        public CatalogItemImageService(CatalogImagesDbContext dbContext,
            ICatalogImageRepository catalogImageRepository,
            IStorageService storageService)
        {
            _dbContext = dbContext;
            _catalogImageRepository = catalogImageRepository;
            _storageService = storageService;
        }

        public async Task AddNewImage(AddNewImageViewModel model)
        {
            var cImage = new CatalogItemImageModel()
            {
                ID = default,
                ImageUri = model.ImageURI,
                ProductID = model.ProductID,
                SizeID = model.Size.ID.GetValueOrDefault(),
                TypeID = model.Type.ID.GetValueOrDefault()
            };

            await _catalogImageRepository.Add(cImage);
        }

        public Task DeleteImage(string fileNameWithExtension) =>
            _storageService.Delete(fileNameWithExtension);

        public Task<List<CatalogItemImageModel>> GetAllImageForProductID(string productID) => _dbContext.Images.Include(i=> i.Size).Include(i=> i.Type).Where(i => i.ProductID == productID).ToListAsync();

        public Task<List<CatalogItemImageModel>> GetAllImageForProductIDByFiltering(string productID, string typeName = null, string sizeName = null)
        {
            if (typeName != default)
            {
                return _dbContext.Images.Include(i => i.Size).Include(i => i.Type).Where(i=> i.Type.Name == typeName && i.ProductID == productID).ToListAsync();
            }
            else if (sizeName != default)
            {
                return _dbContext.Images.Include(i => i.Size).Include(i => i.Type).Where(i => i.Size.Name == sizeName && i.ProductID == productID).ToListAsync();
            }
            else
            {
                return GetAllImageForProductID(productID);
            }
        }

        public Task ModifyImage(int id, ModifyImageViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveImage(int id)
        {
            var catImage = await _catalogImageRepository.GetByID(id);

            if (catImage == default)
            {
                throw new CImageNotFoundException() { ID = id };
            }

            await DeleteImage(catImage.ImageUri);

            await _catalogImageRepository.Delete(id);
        }

        public Task UploadImage(string fileNameWithExtension, IFormFile file)
         => UploadImage("", fileNameWithExtension, file);

        public Task UploadImage(string productID, string fileNameWithExtension, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            return _storageService.UploadWithContainerExtension(productID,fileNameWithExtension, stream);
        }
    }
}
