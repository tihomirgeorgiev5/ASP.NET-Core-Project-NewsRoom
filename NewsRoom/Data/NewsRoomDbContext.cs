using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsRoom.Data.Models;

namespace NewsRoom.Data
{
    public class NewsRoomDbContext : IdentityDbContext<User>
    {
       
        public NewsRoomDbContext(DbContextOptions<NewsRoomDbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Journalist> Journalists { get; init; }

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
                .Entity<News>()
                .HasOne(n => n.Category)
                .WithMany(n => n.News)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<News>()
                .HasOne(n => n.Journalist)
                .WithMany(j => j.News)
                .HasForeignKey(n => n.JournalistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Journalist>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Journalist>(j => j.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
