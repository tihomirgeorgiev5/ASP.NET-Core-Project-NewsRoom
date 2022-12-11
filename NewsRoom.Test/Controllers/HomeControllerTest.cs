using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using NewsRoom.Controllers;
using NewsRoom.Data.Models;
using NewsRoom.Models.Home;
using NewsRoom.Services.News;
using NewsRoom.Services.Statistics;
using NewsRoom.Test.Mocks;
using System.Linq;
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

            var news = Enumerable
                .Range(0, 10)
                .Select(i => new ANews());

            data.News.AddRange(news);
            data.Users.Add(new User());
            data.SaveChanges();

            var newsService = new NewsService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(newsService, statisticsService);

            // Act
            var result = homeController.Index();


            // Assert
            /* Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.News.Count);
            Assert.Equal(10, indexViewModel.TotalNews);
            Assert.Equal(1, indexViewModel.TotalReaders); */

            result
               .Should()
               .NotBeNull()
               .And.BeAssignableTo<ViewResult>()
               .Which
               .Model
               .As<IndexViewModel>()
               .Invoking(model =>
               {
                   model.News.Should().HaveCount(3);
                   model.TotalNews.Should().Be(10);
                   model.TotalReaders.Should().Be(1);
               });

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
