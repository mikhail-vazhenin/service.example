using NUnit.Framework;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Services.Statistics;
using Service.Example.YaAudience.Tools.Handlers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Example.YaAudience.Tests.Integration
{
    [Category("Integration")]
    public class CookieMakerTests
    {
        PassportAuthenticator _target;
        CookieContainer _cookieContainer;

        [SetUp]
        public void Setup()
        {
            _cookieContainer = new CookieContainer();
            _target = new PassportAuthenticator(Refit.RestService.For<IYandexPassportClient>(new HttpClient(new CookieHandler(_cookieContainer))
            {
                BaseAddress = new Uri("https://passport.yandex.ru")
            }),
                _cookieContainer,
                 "", "");
        }

        [Test]
        public async Task WhenTryGetCookie_CookieCreated()
        {
            await _target.Authenticate();

            var cookie = _cookieContainer.GetCookies(new Uri("http://yandex.ru"));
            Assert.NotNull(cookie);
            Assert.Positive(cookie.Count);
        }
    }
}
