using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;

namespace Service.Example.YaAudience.Interfaces.Clients
{
    public interface IAudiencePageClient
    {
        [Get("/")]
        Task<string> GetAudiencePage();

        [Post("/i-proxy/i-audience-api/getStatData?lang=ru")]
        Task<JObject> GetSegmentStatistics([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
    }
}
