using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Models;
using Web.Gtw.Models.ViewModels.Catalog;

namespace Web.Gtw.Services.Abstractions
{
    public interface ICatalogRepository
    {
        Task<PaginationViewModel<CatalogItem>> GetItems(int skip, int take);
    }
}
