using CatalogService.API.Data;
using CatalogService.API.Services.Service.Abstractions;
using CatalogService.API.ViewModels.Products;
using CatalogService.API.ViewModels.Products.SearchableExtension;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Implementations
{
    public class SearchService : ISearchService
    {
        private ProductDbContext _context;

        public SearchService(ProductDbContext context)
        {
            _context = context;
        }

        public long GetAllSearchedItemsCount(string query)
        {
            throw new NotImplementedException();
        }

        public List<CatalogItem> Search(string query)
        {
            throw new NotImplementedException();
        }

        public List<PaginationViewModel<CatalogItem>> SearchPaginationed(int skip, int take, string query)
        {
            throw new NotImplementedException();
        }

        public ContainsAllCountModel<CatalogItem> SearchWithAllCount(string query)
        {
            throw new NotImplementedException();
        }
    }
}
