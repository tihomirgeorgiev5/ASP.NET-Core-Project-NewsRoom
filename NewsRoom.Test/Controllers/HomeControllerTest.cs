using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Data.Models;
using NewsRoom.Services.News;
using NewsRoom.Services.News.Models;
using NewsRoom.Services.Statistics;
using NewsRoom.Test.Mocks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NewsRoom.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/Error")
                .To<HomeController>(n => n.Index())
                .Which(controller => controller
                      .WithData(GetNews()))
                .ShouldReturn()
                .View(view => view
                      .WithModelOfType<List<LatestNewsServiceModel>>()
                      .Passing(m => m.Should().HaveCount(3)));
   
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

        private static IEnumerable<ANews> GetNews()
            => Enumerable.Range(0, 10).Select(i => new ANews());

    }
}
