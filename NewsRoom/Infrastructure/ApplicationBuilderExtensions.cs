using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using System.Linq;

namespace NewsRoom.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app
                .ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<NewsRoomDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

       private static void SeedCategories(NewsRoomDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "България"},
                new Category { Name = "Свят"},
            });

            data.SaveChanges();
        }

    }
}
