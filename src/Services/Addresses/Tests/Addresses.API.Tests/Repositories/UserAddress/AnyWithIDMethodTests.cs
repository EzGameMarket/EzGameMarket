using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class AnyWithIDMethodTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(5, false)]
        [InlineData(-1, false)]
        public async void AnyWithID_ShouldReturnSuccess(int id, bool expected)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{expected}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            //Act
            var repo = new UserAddressRepository(dbContext);
            var actual = await repo.AnyWithID(id);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
