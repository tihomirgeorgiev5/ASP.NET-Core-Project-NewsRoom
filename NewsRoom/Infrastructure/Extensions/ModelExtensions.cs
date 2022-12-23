using NewsRoom.Services.News.Models;

namespace NewsRoom.Infrastructure.Extensions
{
    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this INewsModel aNews)
            => aNews.Area + "-" + aNews.Title + "-" + aNews.Date;
    }
}
