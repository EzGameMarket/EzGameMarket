using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudGamingSupport.API.Services.Repositories.Abstractions
{
    public interface ICloudGamingSupportRepository
    {
        Task<CloudGamingSupported> GetFromProductID(string productID);

        Task<CloudGamingSupported> Get(int id);

        Task Add(CloudGamingSupported model);

        Task<bool> AnyWithID(int id);
        Task<bool> AnyWithProductID(string productID);

        Task Modify(int id, CloudGamingSupported model);
        Task AddProviderToGame(ProviderModifyForGameViewModel model);
        Task RemoveProviderFromGame(ProviderModifyForGameViewModel model);
    }
}
