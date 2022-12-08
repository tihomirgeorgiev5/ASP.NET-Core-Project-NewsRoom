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
            using var data = DatabaseMock.Instance;

            data.Journalists.Add(new Journalist
            {
                UserId = "TestUserId"
            });

            data.SaveChanges();

            var journalistService = new JournalistService(data);
        }
    }
}
