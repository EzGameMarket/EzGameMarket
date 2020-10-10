using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class GetByIDMethodTests
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(5, true)]
        [InlineData(-1, true)]
        public async void GetAddress_ShouldReturnSuccess(int id, bool isNull)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{isNull}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var actual = await repo.GetByID(id);

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
