using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Services.Repositories.Abstractions
{
    public interface ICloudGamingProviderRepository
    {
        Task<CloudGamingProvider> Get(int id);
        Task Add(CloudGamingProvider model);
        Task Modify(int id, CloudGamingProvider model);

        Task<bool> AlreadyAddedToSupportedGames(ProviderModifyForGameViewModel model);

        Task<List<string>> GetSupportedGames(int id);

        Task<CloudGamingProvider> GetByProviderName(string providerName);
    }
}
