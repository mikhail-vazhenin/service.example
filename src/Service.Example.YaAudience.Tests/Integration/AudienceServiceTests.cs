using System;
using System.Collections.Generic;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Models.Audience;
using Service.Example.YaAudience.Tools.Handlers;
using NUnit.Framework;
using Refit;

namespace Service.Example.YaAudience.Tests.Integration
{
    [Category("Integration")]
    public class AudienceServiceTests
    {
        IOAuthClient _oauth;

        IAudienceClient _target;

        [SetUp]
        public void Setup()
        {
            _oauth = RestService.For<IOAuthClient>("https://oauth.yandex.ru");
            _target = RestService.For<IAudienceClient>(
                new System.Net.Http.HttpClient(new OAuthHandler(_oauth,
                    "", "", ""))
                {
                    BaseAddress = new Uri("https://api-audience.yandex.ru/v1/management")
                });
        }

        [Test]
        public void WhenTryGetSegments_SegmentsReceived()
        {
            var segments = _target.GetSegments().Result;
            Assert.NotNull(segments);
        }
    }
}