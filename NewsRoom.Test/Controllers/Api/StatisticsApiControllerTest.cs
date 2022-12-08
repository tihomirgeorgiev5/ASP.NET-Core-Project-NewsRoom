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

            
        }
    }
}
