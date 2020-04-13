using CloudGamingSupport.API.Controllers;
using CloudGamingSupport.API.Exceptions;
using CloudGamingSupport.API.Models;
using CloudGamingSupport.API.Services.Repositories.Implementations;
using CloudGamingSupport.API.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudGamingSupport.API.Tests.API.Controllers.CgProvider
{
    public class GetProductsActionTests
    {
        

        [Fact]
        public async void GetProducts_ShouldReturnSuccessAnd2ProviderForID1()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = 1;

            var expectedProductsCount = 2;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.GetSupportedProducts(id);
            var item = await repo.GetSupportedGames(id);

            //Assert
            Assert.NotNull(item);
            Assert.Equal(expectedProductsCount, item.Count);
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<List<string>>>(actionResult);
            var value = Assert.IsAssignableFrom<List<string>>(actionResult.Value);
            Assert.NotNull(value);
            Assert.Equal(expectedProductsCount, value.Count);
        }

        [Fact]
        public async void GetProducts_ShouuldReturnBadRequestForMinus1()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = -1;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.GetSupportedProducts(id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProducts_ShouuldReturnNotFoundForID5()
        {
            //Arange
            var dbContext = new FakeCGDbContext(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName+"-"+ System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.GUID.ToString());
            var repo = new CloudGamingProviderRepository(dbContext.DbContext);

            var id = 5;

            //Act
            var controller = new CgProviderController(repo);
            var actionResult = await controller.GetSupportedProducts(id);
            Task result() => repo.GetSupportedGames(id);

            //Assert
            await Assert.ThrowsAsync<CgNotFoundException>(result);
            Assert.NotNull(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
