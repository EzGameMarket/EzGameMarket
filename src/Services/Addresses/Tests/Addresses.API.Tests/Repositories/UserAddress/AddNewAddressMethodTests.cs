using Addresses.API.Data;
using Addresses.API.Exceptions;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.API.ViewModels;
using Addresses.Tests.FakeImplementations;
using Xunit;

namespace Addresses.API.Tests.Repositories.UserAddress
{
    public class AddNewAddressMethodTests
    {
        [Theory]
        [InlineData("test", false, 2)]
        [InlineData("asd", true, 1)]
        public async void Add_ShouldSuccess(string userID, bool expectedToBeDefault, int excpectedAllCount)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{excpectedAllCount}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var expectedFirstName = "asdasdasd as asd add asd as";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = userID,
                NewAddress = new Models.AddressModel()
                {
                    FirstName = expectedFirstName
                },
                SetToDefault = expectedToBeDefault
            };

            //Act
            var repo = new UserAddressRepository(dbContext);
            await repo.AddAddressForUser(model);
            var actual = await repo.GetAddressesForUser(userID);
            var actualDefault = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(excpectedAllCount, actual.Count);

            if (expectedToBeDefault == true)
            {
                Assert.Equal(expectedFirstName, actualDefault.FirstName);
            }
        }

        [Fact]
        public async void AddCreateNewUserAndNotSetDefault_ShouldSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "asdadsasd";
            var expectedFirstName = "asdasdasd as asd add asd as";

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = userID,
                NewAddress = new Models.AddressModel()
                {
                    FirstName = expectedFirstName
                },
                SetToDefault = false
            };

            //Act
            var repo = new UserAddressRepository(dbContext);
            await repo.AddAddressForUser(model);
            var actual = await repo.GetAddressesForUser(userID);
            var actualDefault = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actual);
            Assert.Null(actualDefault);
        }

        [Fact]
        public async void AddShouldThrowAlreadyExists()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var model = new AddNewAddressToUserViewModel()
            {
                UserID = "asd",
                NewAddress = new Models.AddressModel()
                {
                    ID = 1
                },
                SetToDefault = false
            };

            //Act
            var repo = new UserAddressRepository(dbContext);
            var uploadTask = repo.AddAddressForUser(model);

            //Assert
            await Assert.ThrowsAsync<AddressAlreadyExistsWithIDException>(() => uploadTask);
        }
    }
}