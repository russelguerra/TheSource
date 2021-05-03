using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TheSource.Areas.Identity.IdentityHostingStartup))]

namespace TheSource.Areas.Identity
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