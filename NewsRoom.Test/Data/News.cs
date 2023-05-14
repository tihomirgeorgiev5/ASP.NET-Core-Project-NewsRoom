using NewsRoom.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Test.Data
{
    public static class News
    {
        public static IEnumerable<NewsRoom.Data.Models.News> TenPublicNews
           => Enumerable.Range(0, 10).Select(i => new NewsRoom.Data.Models.News
           {
               IsPublic = true
           });

    }
}
