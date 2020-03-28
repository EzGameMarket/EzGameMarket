using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.CloudGaming.Abstractions;

namespace WebMVC.ViewModels.CloudGaming
{
    public class CloudGamingSupport : ICloudGamingSupport
    {
        public string Name { get; set; }
        public bool Support { get; set; }
        public string DownloadUrl { get; set; }
        public string ProductSupportUrl { get; set; }
        public string BaseUrl { get; set; }
    }
}
