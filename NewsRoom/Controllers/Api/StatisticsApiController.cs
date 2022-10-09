using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models.Api.Statistics;
using System.Linq;

namespace NewsRoom.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly NewsRoomDbContext data;

        public StatisticsApiController(NewsRoomDbContext data)
            => this.data = data;

        public StatisticsResponseModel GetStatistics()
        {
            var totalNews = this.data.News.Count();
            var totalReaders = this.data.Users.Count();

            var statistics = new StatisticsResponseModel
            {
                TotalNews = totalNews,
                TotalReaders = totalReaders,   
                TotalWriters = 0,
            };

            return statistics;
        }



    }
}
