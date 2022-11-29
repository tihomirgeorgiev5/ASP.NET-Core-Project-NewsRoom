using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsRoom.Controllers;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null, Mock.Of<IMapper>(), null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
