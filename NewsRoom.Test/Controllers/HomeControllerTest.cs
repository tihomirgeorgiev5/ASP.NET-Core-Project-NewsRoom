using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Services.News.Models;
using System.Collections.Generic;
using Xunit;

using static NewsRoom.Test.Data.News;

namespace NewsRoom.Test.Controllers

   
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
        {
            MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(n => n.Index())
                .Which(controller => controller
                      .WithData(TenPublicNews))
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

    }
}
