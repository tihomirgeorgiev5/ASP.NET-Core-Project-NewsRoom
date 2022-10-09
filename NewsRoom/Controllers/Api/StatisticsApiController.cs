using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;

namespace NewsRoom.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly NewsRoomDbContext data;

        public StatisticsApiController(NewsRoomDbContext data)
            => this.data = data;



    }
}
