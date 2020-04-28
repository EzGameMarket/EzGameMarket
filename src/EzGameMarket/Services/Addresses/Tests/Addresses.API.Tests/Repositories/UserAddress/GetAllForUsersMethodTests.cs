using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class GetAllForUsersMethodTests
    {
        [Theory]
        [InlineData("kriszw", 2)]
        [InlineData("test", 1)]
        [InlineData("", 0)]
        public async void GetAllAddressForUserID_ShouldReturnSuccess(string userID, int expectedCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{expectedCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var actual = await repo.GetAddressesForUser(userID);

            //Assert


            if (expectedCount != 0)
            {
                Assert.NotNull(actual);
                Assert.Equal(expectedCount, actual.Count);
            }
            else
            {
                Assert.Null(actual);
            }

        }
    }
}
