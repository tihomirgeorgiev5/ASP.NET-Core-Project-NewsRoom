using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsRoom.Data;

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
            return app;
        }

       

    }
}
