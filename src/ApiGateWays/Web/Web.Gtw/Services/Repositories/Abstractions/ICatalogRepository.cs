using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;
using Web.Gtw.Models.ViewModels.Catalog.SingleProduct;

namespace Web.Gtw.Services.Repositories.Abstractions
{
    public interface ICatalogRepository
    {
        Task<PaginationViewModel<CatalogItem>> GetItems(int skip, int take);
        Task<PaginationViewModel<CatalogItem>> Filter(int skip, int take, IEnumerable<string> brands, IEnumerable<string> categories, IEnumerable<string> tags);
        Task<IEnumerable<CatalogItem>> GetRecomended(string userID);
        Task<Product> GetDetail(string productID);
    }
}
