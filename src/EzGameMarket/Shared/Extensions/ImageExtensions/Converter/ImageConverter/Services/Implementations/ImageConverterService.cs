using ImageConverter.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter.Services.Implementations
{
    public class ImageConverterService : IImageConverterService
    {
        public Task<Stream> Convert(Stream stream, string extension)
        {
            throw new NotImplementedException();
        }
    }
}
