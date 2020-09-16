using Service.Example.YaAudience.Models.Audience;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Example.YaAudience.Interfaces.Clients
{
    public interface IOAuthClient
    {
        [Post("/token")]
        Task<TokenInfo> RefreshToken([Body(BodySerializationMethod.UrlEncoded)]Dictionary<string, object> data);
    }   

}
