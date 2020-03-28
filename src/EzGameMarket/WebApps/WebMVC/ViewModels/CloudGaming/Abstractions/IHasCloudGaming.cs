using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.CloudGaming.Abstractions
{
    public interface IHasCloudGaming
    {
        IEnumerable<ICloudGamingSupport> CloudGamingSupports { get; set; }
    }
}
