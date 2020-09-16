using System.Threading.Tasks;
using Service.Example.YaAudience.Models;
using Service.Example.YaAudience.Services.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace Service.Example.YaAudience.Controllers
{
    [Route("statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsPageClient _statisticReader;
        public StatisticsController(StatisticsPageClient statisticReader)
        {
            _statisticReader = statisticReader;
        }

        [HttpGet("{externalAudienceId}")]
        public async Task<AudienceStatistic> Get(string externalAudienceId)
        {
            return await _statisticReader.GetStatistic(externalAudienceId).ConfigureAwait(false);
        }
    }
}
