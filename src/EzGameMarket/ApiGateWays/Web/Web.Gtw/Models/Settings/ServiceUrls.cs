using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Models.Settings
{
    public class ServiceUrls
    {
        public static ServiceUrls Services { get; set; }

        public string Identity { get; set; }
        public string Cart { get; set; }
        public string Catalog { get; set; }
        public string Order { get; set; }
    }
}
