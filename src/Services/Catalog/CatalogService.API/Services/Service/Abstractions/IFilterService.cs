using CatalogService.API.ViewModels.Products;
using CatalogService.API.ViewModels.Products.SearchableExtension;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Abstractions
{
    public interface IFilterService
    {
        Task<ContainsAllCountModel<CatalogItem>> FilterByCategoriesAsync(int skip, int take, IEnumerable<string> categories);
        Task<ContainsAllCountModel<CatalogItem>> FilterByTagsAsync(int skip, int take, IEnumerable<string> tags);
        Task<ContainsAllCountModel<CatalogItem>> FilterCategoriesAndTagsAsync(int skip, int take, IEnumerable<string> categories, IEnumerable<string> tags);
    }
}
