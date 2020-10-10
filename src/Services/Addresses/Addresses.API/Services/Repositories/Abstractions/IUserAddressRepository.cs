using Addresses.API.Models;
using Addresses.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.API.Services.Repositories.Abstractions
{
    public interface IUserAddressRepository
    {
        Task<AddressModel> GetDefaultForUser(string userID);
        Task<AddressModel> GetByID(int id);
        Task<List<AddressModel>> GetAddressesForUser(string userID);
        Task SetDefaultAddress(string userID, int addressID);
        Task AddAddressForUser(AddNewAddressToUserViewModel model);
        Task UpdateAddress(int addressID, AddressModel model);
        Task DeleteAddress(int addressID);

        Task<bool> AnyWithID(int id);
    }
}
