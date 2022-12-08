using NewsRoom.Data.Models;
using NewsRoom.Services.Journalists;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Services
{
    public class JournalistServiceTest
    {
        [Fact]
        public void IsJournalistShouldReturnTrueWhenUserIsJournalist()
        {
            // Arrange
            const string userId = "TestUserId";
            using var data = DatabaseMock.Instance;

            data.Journalists.Add(new Journalist
            {
                UserId = userId
            });

            data.SaveChanges();

            var journalistService = new JournalistService(data);

            // Act
            var result = journalistService.IsJournalist(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsJournalistShouldReturnFalseWhenUserIsNotJournalist()
        {
            // Arrange
            
            using var data = DatabaseMock.Instance;

            data.Journalists.Add(new Journalist
            {
                UserId = "TestUserId"
            });

            data.SaveChanges();

            var journalistService = new JournalistService(data);

            // Act
            var result = journalistService.IsJournalist("AnotherUserId");

            // Assert
            Assert.False(result);
        }
    }
}
