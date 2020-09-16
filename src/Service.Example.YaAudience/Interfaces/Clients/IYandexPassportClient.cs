using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;

namespace Service.Example.YaAudience.Interfaces.Clients
{
    public interface IYandexPassportClient
    {
        [Headers(
            "Upgrade-Insecure-Requests:1",
            "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
            "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8")]
        [Get("/profile")]
        Task<string> GetProfilePage();


        [Headers(
          "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
          "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8")]
        [Post("/registration-validations/auth/multi_step/start")]
        Task<JObject> SendAuthLogin([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);


        [Post("/registration-validations/auth/multi_step/commit_password")]
        Task SendAuthPass([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);


        //[Post("/registration-validations/get.addresses")]
        //Task<HttpResponseMessage> IsAuthenticatied([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);
        [Headers(
            "Upgrade-Insecure-Requests:1",
            "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8",
            "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8")]
        [Get("/profile")]
        Task<HttpResponseMessage> GetProfilePageResponse();
    }
}
