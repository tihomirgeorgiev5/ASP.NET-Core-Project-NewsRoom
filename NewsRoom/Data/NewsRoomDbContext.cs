using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure.Data.Common.Models.Contracts;
using NewsRoom.Infrastructure.Data;
using NewsRoom.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;

namespace NewsRoom.Data
{
    public class NewsRoomDbContext : IdentityDbContext<User>
    {
       
        public NewsRoomDbContext(DbContextOptions<NewsRoomDbContext> options)
            : base(options)
        {
        }

        // DbSet represent each table entity in the database
        public DbSet<News> News { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Journalist> Journalists { get; init; }

        public DbSet<FaqEntity> Faqs { get; init; }



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

            builder
                .Entity<FaqEntity>()
                .HasData(SeedUserData<FaqEntity>(@"DataSeed/faqs.json"));

            base.OnModelCreating(builder);
        }

        public List<T> SeedUserData<T>(string filePath) where T : class 
        {
            var model = new List<T>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                model = JsonConvert.DeserializeObject<List<T>>(json);
            }
            return model;

        } 
    }
}
