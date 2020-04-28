using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Addresses.Tests.FakeImplementations;
using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.API.Exceptions;
using System.Runtime.InteropServices;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class DeleteAddressMethodTests
    {
        [Theory]
        [InlineData(1,"kriszw",2)]
        [InlineData(2,"kriszw",1)]
        [InlineData(3,"test",null)]
        public async void Delete_ShouldReturnSuccess(int id, string expectedUserID, int? expectedNewDefAddressID)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{expectedUserID}-{expectedNewDefAddressID}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            await repo.DeleteAddress(id);
            var actual = await repo.GetByID(id);
            var defAddresses = await repo.GetDefaultForUser(expectedUserID);

            //Assert
            Assert.Null(actual);
            if (expectedNewDefAddressID != default)
            {
                Assert.NotNull(defAddresses);
                Assert.Equal(expectedNewDefAddressID, defAddresses.ID);
            }
            else
            {
                Assert.Null(defAddresses);
            }
        }

        [Theory]
        [InlineData(100, typeof(AddressNotFoundByIDException))]
        public async void Delete_ShouldThrowNotFoundException(int id, Type expecetedExceptionType)
        {
            //Arrange 
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var deleteTask = repo.DeleteAddress(id);

            //Assert
            await Assert.ThrowsAsync(expecetedExceptionType, () => deleteTask);
        }
    }
}
