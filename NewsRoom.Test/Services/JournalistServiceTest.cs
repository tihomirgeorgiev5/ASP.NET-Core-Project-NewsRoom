using NewsRoom.Data.Models;
using NewsRoom.Services.Journalists;
using NewsRoom.Test.Mocks;
using Xunit;

namespace NewsRoom.Test.Services
{
    public class JournalistServiceTest
    {
        private const string UserId = "TestUserId";
        [Fact]
        public void IsJournalistShouldReturnTrueWhenUserIsJournalist()
        {
            // Arrange
            var journalistService = GetJournalistService();

            // Act
            var result = journalistService.IsJournalist(UserId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsJournalistShouldReturnFalseWhenUserIsNotJournalist()
        {
            // Arrange
            var journalistService = GetJournalistService();

            // Act
            var result = journalistService.IsJournalist("AnotherUserId");

            // Assert
            Assert.False(result);
        }

        private static IJournalistService GetJournalistService()
        {
            
            var data = DatabaseMock.Instance;

            data.Journalists.Add(new Journalist
            {
                UserId = UserId
            });

            data.SaveChanges();

            return new JournalistService(data);
        }
    }
}
