using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Service.Example.YaAudience.Configuration;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Models;
using Service.Example.YaAudience.Services.Converters;
using Service.Example.YaAudience.Services.Statistics;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.Example.YaAudience.Services.Statistics
{
    /// <summary>
    /// Получение статистики с формы Аудиторий
    /// </summary>
    public class StatisticsPageClient
    {
        private readonly IAudiencePageClient _audiencePageClient;
        private readonly StatisticsJsonParser _parser;
        private readonly PassportAuthenticator _passportAuthenticator;

        public StatisticsPageClient(IAudiencePageClient audiencePageClient, PassportAuthenticator passportAuthenticator, StatisticsJsonParser parser)
        {
            _audiencePageClient = audiencePageClient;
            _parser = parser;
            _passportAuthenticator = passportAuthenticator;
        }


        public async Task<AudienceStatistic> GetStatistic(string audienceId)
        {
            await _passportAuthenticator.Authenticate();

            var skv = await GetSkvCode();

            var parameters = new Dictionary<string, object>
            {
                ["args"] = $"[\"{audienceId}\"]",
                ["key"] = skv,
                ["lang"] = "ru"
            };

            var segmentJson = await _audiencePageClient.GetSegmentStatistics(parameters).ConfigureAwait(false);
            var statistic = _parser.Parse(segmentJson);
            return statistic;
        }


        private async Task<string> GetSkvCode()
        {
            var audiencePageContent = await _audiencePageClient.GetAudiencePage().ConfigureAwait(false);

            var html = new HtmlDocument();
            html.LoadHtml(audiencePageContent);

            var bemString = html.DocumentNode.SelectSingleNode("//html//body").Attributes["data-bem"].Value;
            var bemJson = (JObject)JsonConvert.DeserializeObject(bemString);
            var skv = bemJson.SelectToken("i-global.jsParams.i-api-request.skv2").Value<string>();

            return skv;
        }

    }
}
