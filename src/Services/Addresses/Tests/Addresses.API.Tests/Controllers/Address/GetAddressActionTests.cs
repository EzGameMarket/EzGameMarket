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
    public class GetAddressActionTests
    {
        [Fact]
        public async void Get_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var addressID = 1;

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddress(addressID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<AddressModel>>>(actionResult);
            var response = Assert.IsAssignableFrom<APIResponse<AddressModel>>(actionResult.Value);
            Assert.True(response.Success);
            var actual = Assert.IsAssignableFrom<AddressModel>(response.Data);
            Assert.Equal(addressID, actual.ID);
        }

        [Fact]
        public async void Get_ShouldReturnBadRequestForIDMinus1()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var addressID = -1;

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddress(addressID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<AddressModel>>>(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void Get_ShouldReturnNotFound()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var addressID = 100;

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.GetAddress(addressID);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<APIResponse<AddressModel>>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
