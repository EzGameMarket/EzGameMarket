using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IdentityService.API.Areas.Identity.IdentityHostingStartup))]

namespace IdentityService.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}