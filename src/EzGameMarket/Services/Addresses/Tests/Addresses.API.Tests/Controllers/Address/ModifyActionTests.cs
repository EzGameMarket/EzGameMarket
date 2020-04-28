using Addresses.API.Controllers;
using Addresses.API.Data;
using Addresses.API.Models;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Controllers.Address
{
    public class ModifyActionTests
    {
        [Fact]
        public async void Modify_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var id = 1;
            var newFirstName = "Wasdasada";
            var model = new AddressModel()
            {
                ID = id,
                FirstName = newFirstName
            };

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.ModifyAddress(id, model);
            var actualData = await repo.GetByID(id);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(newFirstName, actualData.FirstName);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateAddInvalidTestData))]
        public async void Modify_ShouldFail(int id,AddressModel model, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{id}-{model.GetHashCode()}-{expectedMSG}-{expectedActionResultObjectType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.ModifyAddress(id, model);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateAddInvalidTestData() => new object[][]
        {
            new object[] {1, new AddressModel() { ID = 2 }, "A model IDja és a parmatér ID nem egyezik", typeof(BadRequestObjectResult) },
            new object[] {-1, new AddressModel() { }, "A paraméter cím azonosító nem lehet kisebb mint 1", typeof(BadRequestObjectResult) },
            new object[] {100, new AddressModel() { ID = 100 }, "A 100 azonosítóval nem létezik cím a rendszerben", typeof(NotFoundObjectResult) },
        };
    }
}
