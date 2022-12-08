using Moq;
using NewsRoom.Services.Statistics;

namespace NewsRoom.Test.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get 
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalNews = 5,
                        TotalWriters = 10,
                        TotalReaders = 15
                    });

                return statisticsServiceMock.Object;
            }

        }
    }
}
