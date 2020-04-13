using MarketingService.API.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketingService.Tests.FakeImplementations
{
    public class FakeMarketingDbContext
    {
        public static MarketingDbContext CreateDbContext()
        {
            return default;
        }
    }
}
