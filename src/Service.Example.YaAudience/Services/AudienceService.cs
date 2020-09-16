using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Example.YaAudience.Interfaces.Clients;
using Service.Example.YaAudience.Models;
using Newtonsoft.Json.Linq;
using Refit;

namespace Service.Example.YaAudience.Services
{
    /// <summary>
    /// Взаимодействие с API Yandex
    /// </summary>
    public class AudienceService
    {
        const string SegmentPath = "segment.id";

        private readonly IAudienceClient _audienceClient;

        public AudienceService(IAudienceClient audienceClient)
        {
            _audienceClient = audienceClient;
        }

        public async Task<string> CreateSegment(string name, IEnumerable<string> hashs)
        {
            string segmentId;

            using (var stream = GetUploadStream(hashs))
            {
                var part = new StreamPart(stream, name, "application/octet-stream");
                var uploadResponse = await _audienceClient.UploadSegment(part);
                segmentId = GetSegmentId(uploadResponse);
            }

            var segmentParameter = GetSegmentParameter(segmentId, name);
            var confirmationResponse = await _audienceClient.ConfirmSegment(segmentId, segmentParameter);

            return GetSegmentId(confirmationResponse);

        }

        public async Task<SegmentCollection> GetSegments()
        {
            var segmentCollection = await _audienceClient.GetSegments();

            return segmentCollection;
        }

        public async Task<bool> DeleteSegment(string segmentId)
        {
            var deleteResponse = await _audienceClient.DeleteSegment(segmentId);
            return deleteResponse.Value<bool>("success");
        }

        private Stream GetUploadStream(IEnumerable<string> hashs)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var h in hashs)
            {
                builder.AppendLine(h);
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());

            return new MemoryStream(byteArray);
        }

        private string GetSegmentId(JObject @object)
        {
            return @object.SelectToken(SegmentPath).ToObject<string>();
        }

        private object GetSegmentParameter(string segmentId, string name)
        {
            var segmentParameter = new
            {
                segment = new
                {
                    id = segmentId,
                    name = name,
                    hashed = 1,
                    content_type = "mac"
                }
            };

            return segmentParameter;
        }
    }
}
