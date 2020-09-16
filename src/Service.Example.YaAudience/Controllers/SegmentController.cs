using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Example.YaAudience.Models;
using Service.Example.YaAudience.Models.Parameters;
using Service.Example.YaAudience.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Service.Example.YaAudience.Controllers
{
    [Route("segments")]
    [ApiController]
    public class SegmentController : ControllerBase
    {
        private readonly AudienceService _audienceService;

        public SegmentController(AudienceService audienceService)
        {
            _audienceService = audienceService;
        }

        [HttpPost]
        public async Task<string> Create([FromBody]NewSegmentParameters parameters)
        {
            var number = await _audienceService.CreateSegment(parameters.Name, parameters.Hashs).ConfigureAwait(false);
            return number;
        }

        [HttpDelete("{segmentId}")]
        public async Task<bool> Delete(string segmentId)
        {
            var result = await _audienceService.DeleteSegment(segmentId).ConfigureAwait(false);
            return result;
        }

        [HttpGet]
        public async Task<SegmentInfo[]> GetSegments()
        {
            var result = await _audienceService.GetSegments().ConfigureAwait(false);
            return result.Segments;
        }
    }
}