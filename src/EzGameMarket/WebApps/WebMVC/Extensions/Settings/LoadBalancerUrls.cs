using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Extensions.Settings
{
    public class LoadBalancerUrls : ILoadBalancerUrls
    {
        public string MainBalancer { get; set; } = "http://web-gtw:80";
    }
}
