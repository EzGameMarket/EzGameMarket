using CatalogImages.API.Data;
using CatalogImages.API.Models;
using CatalogImages.API.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Services.Repositories.Implementations
{
    public class CatalogItemImageRepository : ICatalogItemImageRepository
    {
        private CatalogImagesDbContext _dbContext;

        public CatalogItemImageRepository(CatalogImagesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddNewImage(CatalogItemImageModel model)
        {
            throw new NotImplementedException();
        }

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

        public Task RemoveImage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
