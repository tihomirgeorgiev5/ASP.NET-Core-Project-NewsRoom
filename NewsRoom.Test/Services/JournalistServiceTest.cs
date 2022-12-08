using NewsRoom.Data;
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
            using var data = this.GetJournalistData();
            
            var journalistService = new JournalistService(data);

            // Act
            var result = journalistService.IsJournalist(UserId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsJournalistShouldReturnFalseWhenUserIsNotJournalist()
        {
            // Arrange
           using var data = this.GetJournalistData();

            var journalistService = new JournalistService(data);

            // Act
            var result = journalistService.IsJournalist("AnotherUserId");

            // Assert
            Assert.False(result);
        }

        private NewsRoomDbContext GetJournalistData()
        {
            
            var data = DatabaseMock.Instance;

            data.Journalists.Add(new Journalist
            {
                UserId = UserId
            });

            data.SaveChanges();

            return data;
        }
    }
}
