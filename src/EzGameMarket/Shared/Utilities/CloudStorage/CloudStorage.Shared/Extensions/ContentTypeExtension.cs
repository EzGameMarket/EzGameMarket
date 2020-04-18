using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Extensions
{
    public static class ContentTypeExtension
    {
        private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string filename)
        {
            if (Provider.TryGetContentType(filename, out string contentType) == false)
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
