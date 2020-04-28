using Addresses.API.Data;
using Addresses.API.Exceptions;
using Addresses.API.Models;
using Addresses.API.Services.Repositories.Abstractions;
using Addresses.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.API.Services.Repositories.Implementations
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private AddressesDbContext _dbContext;

        public UserAddressRepository(AddressesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAddressForUser(AddNewAddressToUserViewModel model)
        {
            if (await AnyWithID(model.NewAddress.ID.GetValueOrDefault()) == true)
            {
                throw new AddressAlreadyExistsWithIDException() { ID = model.NewAddress.ID.GetValueOrDefault() };
            }

            var userAddress = await GetUserAddressesByUserID(model.UserID);

            if (userAddress == default)
            {
                await UploadNewUser(model);
            }
            else
            {
                AppendAddressToExisting(model, userAddress);
            }


            await _dbContext.SaveChangesAsync();
        }

        private static void AppendAddressToExisting(AddNewAddressToUserViewModel model, UserAddresses userAddress)
        {
            userAddress.Addresses.Add(model.NewAddress);

            if (model.SetToDefault)
            {
                userAddress.DefaultAddress = model.NewAddress;
            }
        }

        private async Task UploadNewUser(AddNewAddressToUserViewModel model)
        {
            var newModel = new UserAddresses()
            {
                Addresses = new List<AddressModel>() { model.NewAddress },
                DefaultAddress = model.SetToDefault ? model.NewAddress : default,
                UserID = model.UserID,
            };

            await _dbContext.UserAddresses.AddAsync(newModel);
        }

        public Task<bool> AnyWithID(int id) => _dbContext.Addresses.AnyAsync(a => a.ID.GetValueOrDefault() == id);

        public async Task DeleteAddress(int addressID)
        {
            if (await AnyWithID(addressID) == false)
            {
                throw new AddressNotFoundByIDException() { ID = addressID };
            }

            var userAddress = await GetOtherAddressesForAddressID(addressID);

            if (userAddress.DefaultAddress.ID.GetValueOrDefault() == addressID)
            {
                var newDefAddressID = default(int?);

                if (userAddress.Addresses.Count - 1 > 0)
                {
                    newDefAddressID = userAddress.Addresses.FirstOrDefault(a => a.ID.GetValueOrDefault() != addressID)?.ID;
                }

                userAddress.DefaultAddressID = newDefAddressID;
            }

            var model = await GetByID(addressID);

            _dbContext.Addresses.Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<UserAddresses> GetOtherAddressesForAddressID(int id) => (await _dbContext.UserAddresses.Include(ua => ua.DefaultAddress).Include(ua => ua.Addresses)
                .Where(ua => ua.Addresses.Any(a => a.ID.GetValueOrDefault() == id)).FirstOrDefaultAsync());

        public async Task<List<AddressModel>> GetAddressesForUser(string userID) => (await _dbContext.UserAddresses.Include(ua => ua.Addresses).FirstOrDefaultAsync(ua => ua.UserID == userID))?.Addresses;

        public Task<AddressModel> GetByID(int id) => _dbContext.Addresses.FirstOrDefaultAsync(a => a.ID.GetValueOrDefault() == id);

        public async Task<AddressModel> GetDefaultForUser(string userID) => (await _dbContext.UserAddresses.Include(ua=> ua.DefaultAddress).FirstOrDefaultAsync(ua => ua.UserID == userID))?.DefaultAddress;

        public async Task SetDefaultAddress(string userID, int addressID)
        {
            var address = await GetByID(addressID);

            if (await AnyWithID(addressID) == false)
            {
                throw new AddressNotFoundByIDException() { ID = addressID };
            }

            var userModel = await GetUserAddressesByUserID(userID);

            if (userModel == default)
            {
                throw new AddressesNotFoundForUserIDException() { UserID = userID };
            }

            if (userModel.Addresses.Any(a=> a.ID.GetValueOrDefault() == addressID) == false)
            {
                throw new AddressNotAsignedForUserIDException() { AddressID = addressID, UserID = userID };
            }

            userModel.DefaultAddressID = addressID;
            await _dbContext.SaveChangesAsync();
        }

        private Task<UserAddresses> GetUserAddressesByUserID(string userID) => _dbContext.UserAddresses.Include(ua => ua.Addresses).FirstOrDefaultAsync(ua => ua.UserID == userID);
        
        public async Task UpdateAddress(int addressID, AddressModel model)
        {
            if (await AnyWithID(addressID) == false)
            {
                throw new AddressNotFoundByIDException() { ID = addressID };
            }

            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
