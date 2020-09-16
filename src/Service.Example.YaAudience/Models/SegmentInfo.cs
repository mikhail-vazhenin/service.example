using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.Example.YaAudience.Models
{
    public class SegmentInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }

        public bool Success
        {
            get
            {
                return Status.Equals("processed", StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}
