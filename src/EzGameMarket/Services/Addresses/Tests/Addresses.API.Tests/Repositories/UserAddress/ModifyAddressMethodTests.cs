using Addresses.API.Data;
using Addresses.API.Exceptions;
using Addresses.API.Models;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.API.ViewModels;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class ModifyAddressMethodTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var id = 1;
            var newFirstName = "Varga";
            var model = new AddressModel() 
            {
                ID = id,
                FirstName = newFirstName
            };
            

            //Act
            var repo = new UserAddressRepository(dbContext);
            await repo.UpdateAddress(id, model);
            var actual = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(newFirstName, actual.FirstName);
        }

        [Fact]
        public async void Modify_ShouldThrowNotFoundException()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var id = 100;
            var newFirstName = "Varga";
            var model = new AddressModel()
            {
                ID = id,
                FirstName = newFirstName
            };


            //Act
            var repo = new UserAddressRepository(dbContext);
            var updateTask = repo.UpdateAddress(id, model);

            //Assert
            await Assert.ThrowsAsync<AddressNotFoundByIDException>(()=> updateTask);
        }
    }
}
