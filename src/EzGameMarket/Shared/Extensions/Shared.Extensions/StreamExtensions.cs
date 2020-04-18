using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToByteArray(this Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return memoryStream.ToArray();
            }
            else
            {
                using var memStream = new MemoryStream();
                await stream.CopyToAsync(memStream);

                return memStream.ToArray();
            }
        }
    }
}
