using CloudGamingSupport.API.Data;
using CloudGamingSupport.API.Exceptions;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Abstractions;
using CloudGamingSupport.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Services.Repositories.Implementations
{
    public class CloudGamingSupportRepository : ICloudGamingSupportRepository
    {
        private readonly CGDbContext dbContext;
        private readonly ICloudGamingProviderRepository _cloudGamingProviderRepository;

        public CloudGamingSupportRepository(CGDbContext _dbContext, ICloudGamingProviderRepository cloudGamingProviderRepository)
        {
            dbContext = _dbContext;
            _cloudGamingProviderRepository = cloudGamingProviderRepository;
        }

        public async Task Add(CloudGamingSupported model)
        {
            if (model.ID.HasValue)
            {
                var res = await CheckIfIdInDatabase(model.ID.Value);

                if (res)
                {
                    throw new CgAlreadyInDataBaseException() { ID = model.ID.Value, ProductID = model.ProductID };
                }
            }

            var resForProduct = await CheckIfProductInDatabase(model.ProductID);

            if (resForProduct)
            {
                throw new CgAlreadyInDataBaseException() { ID = model.ID.GetValueOrDefault(), ProductID = model.ProductID };
            }

            await dbContext.Games.AddAsync(model);

            await dbContext.SaveChangesAsync();
        }

        public async Task Modify(int id, CloudGamingSupported model)
        {
            var item = await Get(id);

            if (item == default)
            {
                throw new CgNotFoundException() { ID = id, ProductID = model.ProductID};
            }

            dbContext.Entry(item).CurrentValues.SetValues(model);

            await dbContext.SaveChangesAsync();
        }

        public async Task AddProviderToGame(AddProviderForGameViewModel model)
        {
            var item = await GetFromProductID(model.ProductID);

            if (item == default)
            {
                throw new CgNotFoundException() { ID = -1, ProductID = model.ProductID } ;
            }

            var provider = await _cloudGamingProviderRepository.Get(model.ProviderID);

            if (await _cloudGamingProviderRepository.AlreadyAddedToSupportedGames(model))
            {
                throw new ProviderAlreadyAddedForProductException() { Model = model };
            }

            if (provider == default)
            {
                throw new CgProviderNotFoundException() { ProviderID = model.ProviderID } ;
            }

            provider.SupportedGames.Add(new CloudGamingProvidersAndGames() { CloudGamingProviderID = model.ProviderID, CloudGamingSupportedID = item.ID.GetValueOrDefault() });

            var t = dbContext.ChangeTracker;

            await dbContext.SaveChangesAsync();
        }

        private async Task<bool> CheckIfIdInDatabase(int id)
        {
            var item = await Get(id);

            return item != default;
        }

        private async Task<bool> CheckIfProductInDatabase(string productID)
        {
            var item = await GetFromProductID(productID);

            return item != default;
        }

        public Task<CloudGamingSupported> Get(int id) => dbContext.Games.Include(g=> g.Providers).FirstOrDefaultAsync(g=> g.ID == id);

        public Task<CloudGamingSupported> GetFromProductID(string productID) => dbContext.Games.FirstOrDefaultAsync(g=> g.ProductID == productID);
    }
}
