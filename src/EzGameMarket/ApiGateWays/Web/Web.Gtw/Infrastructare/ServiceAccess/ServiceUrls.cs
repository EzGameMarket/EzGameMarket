using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Gtw.Infrastructare.ServiceAccess.Abstractions;

namespace Web.Gtw.Infrastructare.ServiceAccess
{
    public class ServiceUrls : IServiceUrls
    {
        public string Identity { get; set; }
        public string Cart { get; set; }
        public string Catalog { get; set; }
        public string Order { get; set; }
    }
}
