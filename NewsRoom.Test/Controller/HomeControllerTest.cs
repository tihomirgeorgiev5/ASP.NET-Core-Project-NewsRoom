using Microsoft.AspNetCore.Mvc;
using NewsRoom.Controllers;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Controller
{
    public class HomeControllerTest
    {
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(null, MapperMock.Instance, null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
