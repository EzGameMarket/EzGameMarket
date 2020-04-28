using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class GetDefaultForUserMethodTests
    {
        [Theory]
        [InlineData("kriszw", false)]
        [InlineData("teasdasda", true)]
        [InlineData("", true)]
        public async void GetDefault_ShouldReturnSuccess(string userID, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{isNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var actual = await repo.GetDefaultForUser(userID);

            //Assert
            if (isNull)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
            }
        }
    }
}
