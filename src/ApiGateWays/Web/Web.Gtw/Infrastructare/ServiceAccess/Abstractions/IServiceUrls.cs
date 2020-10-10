using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gtw.Infrastructare.ServiceAccess.Abstractions
{
    public interface IServiceUrls
    {
        string Identity { get; set; }
        string Cart { get; set; }
        string Catalog { get; set; }
        string Order { get; set; }
    }
}
