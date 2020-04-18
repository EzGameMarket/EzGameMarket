using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Extensions.Settings
{
    public class LoadBalancerUrls : ILoadBalancerUrls
    {
        public string MainBalancer { get; set; } = "http://web-gtw:80";
        public string IdentityService { get; set; } = "http://identity-api:80";
    }
}
