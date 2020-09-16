using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Services.Statistics;
using Service.Example.YaAudience.Tools;
using Service.Example.YaAudience.Tools.Handlers;
using NUnit.Framework;

namespace Service.Example.YaAudience.Tests.Integration
{
    [Category("Integration")]
    public class StatisticsPageClientTests
    {
        CookieContainer _cookieContainer;

        [Test]
        public async Task ReadStatisticFromPage_Success()
        {
            _cookieContainer = new CookieContainer();

            PassportAuthenticator authenticator = new PassportAuthenticator(Refit.RestService.For<IYandexPassportClient>(
                new HttpClient(new CookieHandler(_cookieContainer))
                {
                    BaseAddress = new Uri("https://passport.yandex.ru")
                }),
                _cookieContainer, "", "");


            var target = new StatisticsPageClient(Refit.RestService.For<IAudiencePageClient>(
                new HttpClient(new CookieHandler(_cookieContainer))
                {
                    BaseAddress = new Uri("https://audience.yandex.ru")
                }),
                authenticator,
                new YaAudience.Services.Converters.StatisticsJsonParser());

            await authenticator.Authenticate().ConfigureAwait(false);
            var stats = await target.GetStatistic("");

            Assert.IsNotNull(stats);
        }
    }
}
