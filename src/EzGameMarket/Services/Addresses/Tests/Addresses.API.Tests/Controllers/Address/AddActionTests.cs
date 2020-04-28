using Addresses.API.Controllers;
using Addresses.API.Data;
using Addresses.API.Models;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.API.ViewModels;
using Addresses.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Controllers.Address
{
    public class AddActionTests
    {
        [Fact]
        public async void AddAndNotSetDefault_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "kriszw";
            var newFirstName = "'asd";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = userID,
                NewAddress = new AddressModel()
                {
                    ID = default,
                    FirstName = newFirstName
                },
                SetToDefault = false
            };

            var expectedID = (await dbContext.Addresses.MaxAsync(a=> a.ID)) + 1;
            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.UploadAddress(model);
            var actualData = await repo.GetByID(expectedID.GetValueOrDefault());
            var defAddress = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(model.NewAddress.FirstName, actualData.FirstName);
            Assert.Equal(model.NewAddress.LastName, actualData.LastName);

            Assert.NotNull(defAddress);
            Assert.NotEqual(expectedID, defAddress.ID);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Fact]
        public async void AddAndSetDefault_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "kriszw";
            var newFirstName = "'asd";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = userID,
                NewAddress = new AddressModel()
                {
                    ID = default,
                    FirstName = newFirstName
                },
                SetToDefault = true
            };

            var expectedID = (await dbContext.Addresses.MaxAsync(a => a.ID)) + 1;
            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.UploadAddress(model);
            var actualData = await repo.GetByID(expectedID.GetValueOrDefault());
            var defAddress = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(model.NewAddress.FirstName, actualData.FirstName);
            Assert.Equal(model.NewAddress.LastName, actualData.LastName);

            Assert.NotNull(defAddress);
            Assert.Equal(expectedID, defAddress.ID);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Fact]
        public async void AddAndCreateNewUserAndSetToDefault_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var newUserID = "asdsadad";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = newUserID,
                NewAddress = new AddressModel()
                {
                    ID = default,
                    FirstName = "teasdasd"
                },
                SetToDefault = true
            };

            var expectedID = (await dbContext.Addresses.MaxAsync(a => a.ID)) + 1;
            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.UploadAddress(model);
            var actualData = await repo.GetByID(expectedID.GetValueOrDefault());
            var addresses = await repo.GetAddressesForUser(newUserID);
            var defAddress = await repo.GetDefaultForUser(newUserID);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(model.NewAddress.FirstName, actualData.FirstName);
            Assert.Equal(model.NewAddress.LastName, actualData.LastName);

            Assert.Single(addresses);
            Assert.NotNull(defAddress);
            Assert.Equal(expectedID, defAddress.ID);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Fact]
        public async void AddAndCreateNewUserAndNotSetToDefault_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var newUserID = "asdsadad";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = newUserID,
                NewAddress = new AddressModel()
                {
                    ID = default,
                    FirstName = "teasdasd"
                },
                SetToDefault = false
            };

            var expectedID = (await dbContext.Addresses.MaxAsync(a => a.ID)) + 1;
            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.UploadAddress(model);
            var actualData = await repo.GetByID(expectedID.GetValueOrDefault());
            var addresses = await repo.GetAddressesForUser(newUserID);
            var defAddress = await repo.GetDefaultForUser(newUserID);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(model.NewAddress.FirstName, actualData.FirstName);
            Assert.Equal(model.NewAddress.LastName, actualData.LastName);

            Assert.Single(addresses);
            Assert.Null(defAddress);

            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateAddInvalidTestData))]
        public async void Add_ShouldFail(AddNewAddressToUserViewModel model, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{model.GetHashCode()}-{expectedMSG}-{expectedActionResultObjectType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.UploadAddress(model);

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
            new object[] { new AddNewAddressToUserViewModel() { UserID = "", NewAddress = new AddressModel() } , "A UserID nem lehet üres", typeof(BadRequestObjectResult) },
            new object[] { new AddNewAddressToUserViewModel() { UserID = "a" }, "Az új cím model nem lehet null", typeof(BadRequestObjectResult) },
            new object[] { new AddNewAddressToUserViewModel() { UserID = "kriszw", NewAddress = new AddressModel() { ID = 3 } }, "A 3 azonosítóval már létezik cím a rendszerben", typeof(ConflictObjectResult) },
        };
    }
}
