using Addresses.API.Data;
using Addresses.API.Exceptions;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class SetDefaultMethodTests
    {
        [Fact]
        public async void SetDefault_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "kriszw";
            var expecetedDefaultAddressID = 2;

            //Act
            var repo = new UserAddressRepository(dbContext);
            await repo.SetDefaultAddress(userID,expecetedDefaultAddressID);
            var actual = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expecetedDefaultAddressID, actual.ID.GetValueOrDefault());
        }

        [Theory]
        [InlineData("kriszw", 100, typeof(AddressNotFoundByIDException))]
        [InlineData("asd", 1, typeof(AddressesNotFoundForUserIDException))]
        [InlineData("kriszw", 3, typeof(AddressNotAsignedForUserIDException))]
        public async void SetDefault_ShouldThrowNotFoundByIDException(string userID, int addressID, Type expectedExceptionType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{addressID}-{expectedExceptionType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var updateTask = repo.SetDefaultAddress(userID, addressID);

            //Assert
            await Assert.ThrowsAsync(expectedExceptionType,()=> updateTask);
        }
    }
}
