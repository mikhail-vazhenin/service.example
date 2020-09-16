using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Models.Audience;

namespace Service.Example.YaAudience.Tools.Handlers
{
    public class OAuthHandler : HttpClientHandler
    {
        private readonly IOAuthClient _oAuthClient;
        private Task<TokenInfo> _tokenTask;
        private readonly string _refreshToken;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public OAuthHandler(IOAuthClient oAuthClient, string refreshToken, string clientId, string clientSecret)
        {
            _oAuthClient = oAuthClient;

            _refreshToken = refreshToken;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _tokenTask = TakeToken();
        }

        private Task<TokenInfo> TakeToken()
        {
            var token = _oAuthClient.RefreshToken(new Dictionary<string, object>
            {
                { "grant_type" , "refresh_token" },
                { "refresh_token" , _refreshToken },
                { "client_id" , _clientId },
                { "client_secret" , _clientSecret }

            });
            return token;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenTask;
            if (string.IsNullOrEmpty(token.AccessToken)) throw new HttpRequestException("Не удалось получить OAuth токен");

            request.Headers.Add("Authorization", $"Bearer {token.AccessToken}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
