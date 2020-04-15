using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.ImageScaler.Abstractions
{
    public interface IImageResizerService
    {
        Task<Stream> Resize(Stream stream, int width, int height);
        Task<Stream> Resize(byte[] data, int width, int height);
    }
}
