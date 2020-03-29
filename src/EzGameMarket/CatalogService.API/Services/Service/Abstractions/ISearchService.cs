using CatalogService.API.ViewModels.Products;
using CatalogService.API.ViewModels.Products.SearchableExtension;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Abstractions
{
    public interface ISearchService
    {
        long GetAllSearchedItemsCount(string query);
        List<PaginationViewModel<CatalogItem>> SearchPaginationed(int skip, int take, string query);

        List<CatalogItem> Search(string query);

        ContainsAllCountModel<CatalogItem> SearchWithAllCount(string query);
    }
}
