using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsRoom.Controllers;
using Xunit;

namespace NewsRoom.Test.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
<<<<<<< HEAD
            var homeController = new HomeController(null, Mock.Of<IMapper>(), null);
=======
            var homeController = new HomeController(null, null, null);
>>>>>>> parent of ed9ef71 (added MapperMock.Instance in HomeControllerTest)

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
