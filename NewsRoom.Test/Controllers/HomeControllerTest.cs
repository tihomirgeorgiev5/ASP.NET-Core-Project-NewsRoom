using Microsoft.AspNetCore.Mvc;
using NewsRoom.Controllers;
using Xunit;

namespace NewsRoom.Test.Controllers
{
    public class HomeControllerTest
    {
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
