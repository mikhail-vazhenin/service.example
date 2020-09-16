using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Service.Example.YaAudience.Tools;

namespace Service.Example.YaAudience
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(o => o.Limits.MaxRequestBodySize = 30000000)
                .ConfigureAppConfiguration((hostingContext, config) =>
                    Settings.Configure(hostingContext.HostingEnvironment, config, args))
                .ConfigureLogging(b => b.ClearProviders())
                .UseStartup<Startup>();
    }
}
