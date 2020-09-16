using Service.Example.YaAudience.Models;
using Service.Example.YaAudience.Models.Audience;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Example.YaAudience.Interfaces.Clients
{
    public interface IAudienceClient
    {
        [Get("/segments")]
        Task<SegmentCollection> GetSegments();

        [Multipart]
        [Post("/segments/upload_file")]
        Task<JObject> UploadSegment(StreamPart file);

        [Post("/segment/{segmentId}/confirm")]
        Task<JObject> ConfirmSegment(string segmentId, [Body] object segment);

        [Delete("/segment/{segmentId}")]
        Task<JObject> DeleteSegment(string segmentId);

    }
}
