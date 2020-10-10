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
            if (await AnyWithID(model.ID.GetValueOrDefault(-1)))
            {
                throw new CgAlreadyInDataBaseException() { ID = model.ID.Value, ProductID = model.ProductID };
            }

            if (await AnyWithProductID(model.ProductID))
            {
                throw new CgAlreadyInDataBaseException() { ID = model.ID.GetValueOrDefault(), ProductID = model.ProductID };
            }

            var providers = model.Providers;
            model.Providers = default;

            await dbContext.Games.AddAsync(model);

            await dbContext.SaveChangesAsync();

            await UploadProviders(model, providers);
        }

        private async Task UploadProviders(CloudGamingSupported model, List<CloudGamingProvidersAndGames> providers)
        {
            var tasks = providers.Select(async p =>
            {
                await AddProviderToGame(new ProviderModifyForGameViewModel() { ProductID = model.ProductID, ProviderID = p.ID.GetValueOrDefault() });
            });

            await Task.WhenAll(tasks);
        }

        public async Task Modify(int id, CloudGamingSupported model)
        {
            var item = await Get(id);

            if (item == default)
            {
                throw new CgNotFoundException() { ID = id, ProductID = model.ProductID};
            }

            model.ID = id;

            dbContext.Entry(item).CurrentValues.SetValues(model);

            await dbContext.SaveChangesAsync();
        }

        public async Task AddProviderToGame(ProviderModifyForGameViewModel model)
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

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveProviderFromGame(ProviderModifyForGameViewModel model)
        {
            var item = await GetFromProductID(model.ProductID);

            if (item == default)
            {
                throw new CgNotFoundException() { ID = -1, ProductID = model.ProductID };
            }

            var provider = await _cloudGamingProviderRepository.Get(model.ProviderID);

            if (await _cloudGamingProviderRepository.AlreadyAddedToSupportedGames(model) == false)
            {
                throw new ProviderDoesNotAddedToTheGameYetException() { Model = model };
            }

            if (provider == default)
            {
                throw new CgProviderNotFoundException() { ProviderID = model.ProviderID };
            }

            var match = await dbContext.Matches.FirstOrDefaultAsync(m => m.CloudGamingProviderID == model.ProviderID 
                && m.CloudGamingSupportedID == item.ID);

            provider.SupportedGames.Remove(match);

            await dbContext.SaveChangesAsync();
        }

        public Task<CloudGamingSupported> Get(int id) => dbContext.Games.Include(g=> g.Providers).FirstOrDefaultAsync(g=> g.ID == id);

        public Task<CloudGamingSupported> GetFromProductID(string productID) => dbContext.Games.FirstOrDefaultAsync(g=> g.ProductID == productID);

        public Task<bool> AnyWithID(int id) => dbContext.Games.AnyAsync(g=> g.ID == id);

        public Task<bool> AnyWithProductID(string productID) => dbContext.Games.AnyAsync(g => g.ProductID == productID);
    }
}
