using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions.Pagination
{
    public class ReviewPaginationViewModel<T> : PaginationViewModel<T> where T : class
    {
        public ReviewPaginationViewModel(long totalItemsCount, int actualPage, int itemsPerPage, IEnumerable<T> data) : base(totalItemsCount, actualPage, itemsPerPage, data)
        {
        }

        //csillagok átlag értéke
        public double ProductRate { get; set; }
    }
}
