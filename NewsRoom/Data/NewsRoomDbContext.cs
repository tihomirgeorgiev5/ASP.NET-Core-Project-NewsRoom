using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsRoom.Data.Models;

namespace NewsRoom.Data
{
    public class NewsRoomDbContext : IdentityDbContext
    {
        public DbSet<ANews> News { get; init; }
        public NewsRoomDbContext(DbContextOptions<NewsRoomDbContext> options)
            : base(options)
        {
        }
    }
}
