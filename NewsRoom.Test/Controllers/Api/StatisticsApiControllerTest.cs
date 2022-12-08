using NewsRoom.Controllers.Api;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            // Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalNews);
            Assert.Equal(10, result.TotalWriters);
            Assert.Equal(15, result.TotalReaders);
        }
    }
}
