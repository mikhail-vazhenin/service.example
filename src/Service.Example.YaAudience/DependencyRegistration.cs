using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Service.Example.YaAudience.Configuration;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Services;
using Service.Example.YaAudience.Services.Converters;
using Service.Example.YaAudience.Services.Statistics;
using Service.Example.YaAudience.Tools.Handlers;
using System;
using System.Net;

namespace Service.Example.YaAudience
{
    public static class DependencyRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<CookieContainer>();
            services.AddScoped<CookieHandler>();
            services.AddScoped<OAuthHandler>(s =>
            {
                var section = s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-oauth");
                var refreshToken = section.GetValue<string>("RefreshToken");
                var clientId = section.GetValue<string>("ClientId");
                var clientSecret = section.GetValue<string>("ClientSecret");
                return new OAuthHandler(s.GetRequiredService<IOAuthClient>(), refreshToken, clientId, clientSecret);
            });

            services.AddSingleton<StatisticsJsonParser>();

            services.AddScoped<AudienceService>();

            services.AddRefitClient<IYandexPassportClient>()
                .ConfigureHttpClient((s, c) =>
                {
                    var host = s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-passport:host").Value;
                    c.BaseAddress = new Uri(host);
                })
              .ConfigurePrimaryHttpMessageHandler(s => s.GetService<CookieHandler>());

            services.AddRefitClient<IOAuthClient>()
                .ConfigureHttpClient((s, c) =>
                {
                    var host = s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-oauth:host").Value;
                    c.BaseAddress = new Uri(host);
                });

            services.AddRefitClient<IAudienceClient>()
                .ConfigureHttpClient((s, c) =>
                {
                    var host = s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-audience-api:host").Value;
                    c.BaseAddress = new Uri(host);
                })
                .ConfigurePrimaryHttpMessageHandler(s => s.GetRequiredService<OAuthHandler>());


            services.AddRefitClient<IAudiencePageClient>()
                .ConfigureHttpClient((s, c) =>
                {
                    var host = s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-audience:host").Value;
                    c.BaseAddress = new Uri(host);
                })
                .ConfigurePrimaryHttpMessageHandler(s => s.GetRequiredService<CookieHandler>());


            services.AddTransient(s => new StatisticsPageClient(s.GetRequiredService<IAudiencePageClient>(),
                        s.GetRequiredService<PassportAuthenticator>(),
                        s.GetRequiredService<StatisticsJsonParser>()));

            services.AddTransient(s =>
            {
                var passportSection = new HttpClientSection();
                s.GetRequiredService<IConfiguration>().GetSection("clients:yandex-passport").Bind(passportSection);
                return new PassportAuthenticator(s.GetRequiredService<IYandexPassportClient>(),
                    s.GetRequiredService<CookieContainer>(),
                    passportSection.Login,
                    passportSection.Pass);
            });

        }



    }
}
