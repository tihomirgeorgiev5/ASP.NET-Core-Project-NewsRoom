using NewsRoom.Data;
using NewsRoom.Infrastructure.Data.Common;

namespace NewsRoom.Infrastructure.Data.Repositories
{
    public class AppRepository : Repository
    {
        public AppRepository(NewsRoomDbContext context)
            : base(context)
        {
        }
    }
}
