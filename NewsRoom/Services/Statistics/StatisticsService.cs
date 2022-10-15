using NewsRoom.Data;
using System.Linq;

namespace NewsRoom.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly NewsRoomDbContext data;

        public StatisticsService(NewsRoomDbContext data) 
            => this.data = data;
        

        public StatisticsServiceModel Total()
        {
            var totalNews = this.data.News.Count();
            var totalReaders = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalNews = totalNews,
                TotalReaders = totalReaders,

            };
        }
    }
}
