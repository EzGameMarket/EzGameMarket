using ImageConverter.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter.Settings
{
    public class ImageConverterSettings
    {
        public IEnumerable<string> AllowedExtensions { get; set; }

        public int WebpQuality { get; set; } = 20;
    }
}
