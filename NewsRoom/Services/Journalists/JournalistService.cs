using NewsRoom.Data;
using System.Linq;

namespace NewsRoom.Services.Journalists
{
    public class JournalistService : IJournalistService
    {
        private readonly NewsRoomDbContext data;

        public JournalistService(NewsRoomDbContext data) 
            => this.data = data;

        public bool IsJournalist(string userId)
            => this.data
            .Journalists
            .Any(j => j.UserId == userId);
    }
}
