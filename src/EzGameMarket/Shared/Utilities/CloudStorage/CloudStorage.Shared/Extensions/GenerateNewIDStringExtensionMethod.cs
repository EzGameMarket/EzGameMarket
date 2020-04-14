using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.CloudStorage.Shared.Extensions
{
    public static class GenerateNewIDStringExtensionMethod
    {
        public static string GenerateUniqueID(this string value, int lengthOfID)
        {
            var builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(lengthOfID)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        public static string GenerateUniqueID(this string value) => GenerateUniqueID("",11);
    }
}
