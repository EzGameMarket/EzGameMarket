using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.SearchEngine
{
    public class QueryConverter
    {
        private IDictionary<string, IQuery> ConvertedQueries;

        public IQuery Convert(string rawQuery)
        {
            if (ConvertedQueries.ContainsKey(rawQuery))
            {
                return ConvertedQueries[rawQuery];
            }



            return default;
        }
    }
}
