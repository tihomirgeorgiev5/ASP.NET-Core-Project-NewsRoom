using Microsoft.EntityFrameworkCore;
using NewsRoom.Data;
using System;

namespace NewsRoom.Test.Mocks
{
    public static class DatabaseMock
    {
        public static NewsRoomDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<NewsRoomDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new NewsRoomDbContext(dbContextOptions);
            }
        }
    }
}
