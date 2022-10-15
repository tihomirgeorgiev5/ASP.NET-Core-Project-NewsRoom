using Microsoft.AspNetCore.Mvc;
using NewsRoom.Services.Statistics;

namespace NewsRoom.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
        => this.statistics = statistics;
        
        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Total();
            
    }
}
