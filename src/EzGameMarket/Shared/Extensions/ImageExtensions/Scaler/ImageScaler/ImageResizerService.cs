using Shared.Extensions.ImageScaler.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using System.Threading.Tasks;
using System.IO;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;

namespace Shared.Extensions.ImageExtensions.ImageScaler
{
    public class ImageResizerService : IImageResizerService
    {
        public Task<Stream> Resize(Stream stream, int width, int height)
        {
            using var image = Image.Load(stream, out IImageFormat format);

            image.Mutate(s => s.Resize(width,height));

            var outputStream = new MemoryStream();

            image.Save(outputStream, format);

            return Task.FromResult(outputStream as Stream);
        }

        public Task<Stream> Resize(byte[] data, int width, int height)
        {
            using var stream = new MemoryStream(data);

            return Resize(stream, width, height);
        }
    }
}
