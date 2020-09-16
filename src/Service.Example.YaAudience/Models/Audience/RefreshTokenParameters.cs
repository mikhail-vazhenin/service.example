using Newtonsoft.Json;
using Refit;

namespace Dan.MOne.YaAudience.Models.Audience
{
    public class RefreshTokenParameters
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get { return "refresh_token"; } }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }
    }
}
