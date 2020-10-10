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
    public class CloudGamingProviderRepository : ICloudGamingProviderRepository
    {
        private CGDbContext _dbContext;

        public CloudGamingProviderRepository(CGDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CloudGamingProvider model)
        {
            var item = await Get(model.ID.GetValueOrDefault());

            if (item != default)
            {
                throw new CgAlreadyInDataBaseException() { ID = model.ID.GetValueOrDefault() };
            }

            var providerByName = await GetByProviderName(model.Name);

            if (providerByName != default)
            {
                throw new CgAlreadyInDataBaseException() { ID = model.ID.GetValueOrDefault() };
            }

            await _dbContext.Providers.AddAsync(model);

            await _dbContext.SaveChangesAsync();
        }

        public Task<CloudGamingProvider> GetByProviderName(string providerName) => _dbContext.Providers.FirstOrDefaultAsync(p=> p.Name == providerName);

        public async Task<bool> AlreadyAddedToSupportedGames(ProviderModifyForGameViewModel model)
        {
            var provider = await Get(model.ProviderID);

            if (provider == default)
            {
                throw new CgProviderNotFoundException() { ProviderID = model.ProviderID };
            }

            return provider.SupportedGames.Any(s=> s.Game.ProductID == model.ProductID);
        }

        public Task<CloudGamingProvider> Get(int id) => 
            _dbContext.Providers.Include(p=> p.SupportedGames).FirstOrDefaultAsync(p=> p.ID == id);

        public async Task<List<string>> GetSupportedGames(int id)
        {
            var provider = await Get(id);

            if (provider == default)
            {
                throw new CgNotFoundException() { ID = id };
            }

            return provider.SupportedGames.Select(g=> g.Game.ProductID).ToList();
        }

        public async Task Modify(int id, CloudGamingProvider model)
        {
            var item = await Get(id);

            if (item == default)
            {
                throw new CgProviderNotFoundException() { ProviderID = id };
            }

            _dbContext.Entry(item).CurrentValues.SetValues(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
