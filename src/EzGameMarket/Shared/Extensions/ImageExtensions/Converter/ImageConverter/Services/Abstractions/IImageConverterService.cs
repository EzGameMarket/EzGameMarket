using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter.Services.Abstractions
{
    public interface IImageConverterService
    {
        Task<Stream> Convert(Stream stream, string extension);
    }
}
