using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Models.Home;
using NewsRoom.Services.News;
using NewsRoom.Services.Statistics;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var newsService = new NewsService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(newsService, statisticsService);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);
        }
        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(
                null,
                 null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            
        }

    }
}
