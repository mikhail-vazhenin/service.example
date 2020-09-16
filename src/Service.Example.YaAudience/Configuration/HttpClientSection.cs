using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.Example.YaAudience.Configuration
{
    public class HttpClientSection
    {
        [JsonProperty("host")]
        public string Host { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("pass")]
        public string Pass { get; set; }
    }
}
