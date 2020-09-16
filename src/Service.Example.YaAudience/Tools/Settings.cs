using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Service.Example.YaAudience.Tools
{
    public static class Settings
    {
        internal static void Configure(IWebHostEnvironment hostingEnvironment, IConfigurationBuilder config, string[] args)
        {
            config.AddAppSettings(hostingEnvironment);
            config.AddUserSecrets(hostingEnvironment);
            config.AddEnvironmentVariables();
            config.AddCommandArguments(args);
        }

        private static void AddAppSettings(this IConfigurationBuilder config, IWebHostEnvironment hostingEnvironment)
        {
            var environmentName = hostingEnvironment.EnvironmentName;

            config.AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true);
        }

        private static void AddUserSecrets(this IConfigurationBuilder config, IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment()) // different providers in dev
            {
                var appAssembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                if (appAssembly != null)
                {
                    config.AddUserSecrets(appAssembly, optional: true);
                }
            }
        }

        private static void AddCommandArguments(this IConfigurationBuilder configuration, string[] args)
        {
            if (args != null)
            {
                configuration.AddCommandLine(args);
            }
        }
    }
}
