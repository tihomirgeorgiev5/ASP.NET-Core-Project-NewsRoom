using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsRoom.Data.Models;

namespace NewsRoom.Data
{
    public class NewsRoomDbContext : IdentityDbContext
    {
       
        public NewsRoomDbContext(DbContextOptions<NewsRoomDbContext> options)
            : base(options)
        {
        }

        public DbSet<ANews> News { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-SB9MJ7T\SQLEXPRESS;Database=NewsRoom;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ANews>()
                .HasOne(n => n.Category)
                .WithMany(n => n.News)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
