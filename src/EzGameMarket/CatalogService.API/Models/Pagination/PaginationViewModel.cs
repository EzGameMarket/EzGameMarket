using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Models.Pagination
{
    public class PaginationViewModel<TEntity> where TEntity : class
    {
        public PaginationViewModel(long totalItemsCount, int actualPage, int itemsPerPage, IEnumerable<TEntity> data)
        {
            TotalItemsCount = totalItemsCount;
            ActualPage = actualPage;
            ItemsPerPage = itemsPerPage;
            Data = data;
        }

        public long TotalItemsCount { get; set; }
        public int ActualPage { get; set; }
        public int ItemsPerPage { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
