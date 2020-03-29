using CatalogService.API.Data;
using CatalogService.API.Extensions;
using CatalogService.API.Services.Service.Abstractions;
using CatalogService.API.ViewModels.Products;
using CatalogService.API.ViewModels.Products.SearchableExtension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Services.Service.Implementations
{
    public class FilterService : IFilterService
    {
        private ProductDbContext _dbContext;

        public FilterService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContainsAllCountModel<CatalogItem>> FilterByCategoriesAsync(int skip, int take, IEnumerable<string> categories)
        {
            System.Linq.Expressions.Expression<Func<Models.Product, bool>> filterQuery = p => p.Genres.Any(g => categories.Contains(g.Name));
            var longCount = await _dbContext.Products.LongCountAsync(filterQuery);
            var data = await _dbContext.Products.Where(filterQuery).Skip(skip).Take(take).ToListAsync();

            return new ContainsAllCountModel<CatalogItem>(longCount, data.Select(p => p.ToCatalogItem()));
        }

        public async Task<ContainsAllCountModel<CatalogItem>> FilterByTagsAsync(int skip, int take, IEnumerable<string> tags)
        {
            System.Linq.Expressions.Expression<Func<Models.Product, bool>> filterQuery = p => p.Tags.Any(g => tags.Contains(g.Name));
            var longCount = await _dbContext.Products.LongCountAsync(filterQuery);
            var data = await _dbContext.Products.Where(filterQuery).Skip(skip).Take(take).ToListAsync();

            return new ContainsAllCountModel<CatalogItem>(longCount, data.Select(p => p.ToCatalogItem()));
        }

        public async Task<ContainsAllCountModel<CatalogItem>> FilterCategoriesAndTagsAsync(int skip, int take, IEnumerable<string> categories, IEnumerable<string> tags)
        {
            System.Linq.Expressions.Expression<Func<Models.Product, bool>> filterQuery = 
                p => p.Genres.Any(g => categories.Contains(g.Name) || p.Tags.Any(t=> tags.Contains(t.Name)));

            var longCount = await _dbContext.Products.LongCountAsync(filterQuery);
            var data = await _dbContext.Products.Where(filterQuery).Skip(skip).Take(take).ToListAsync();

            return new ContainsAllCountModel<CatalogItem>(longCount, data.Select(p => p.ToCatalogItem()));
        }
    }
}
