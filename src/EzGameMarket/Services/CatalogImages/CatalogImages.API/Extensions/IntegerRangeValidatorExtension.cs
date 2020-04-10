using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogImages.API.Extensions
{
    public static class IntegerRangeValidatorExtension
    {
        public static bool IsInRange(this int value, int bottom, int top) => value >= bottom && value <= top;
    }
}
