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
    public class GetAddressesActionTests
    {
        [Fact]
        public async void GetAddresses_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "kriszw";
            var expectedAddressesCount = 2;

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddressesForUser(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<List<AddressModel>>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<List<AddressModel>>>(actionResult.Value);
            Assert.True(response.Success);
            var actual = Assert.IsAssignableFrom<List<AddressModel>>(response.Data);
            Assert.Equal(expectedAddressesCount, actual.Count);
        }

        [Fact]
        public async void GetAddresses_ShouldReturnBadRequestForEmptyUserID()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "";

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddressesForUser(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<List<AddressModel>>>>(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetAddresses_ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "asdsadasdsa";

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddressesForUser(userID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<List<AddressModel>>>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
