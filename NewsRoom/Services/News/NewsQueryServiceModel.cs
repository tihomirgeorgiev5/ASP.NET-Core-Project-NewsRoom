using System.Collections.Generic;

namespace NewsRoom.Services.News
{
    public class NewsQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int NewsPerPage { get; init; }

        public int TotalNews { get; init; }

        public IEnumerable<NewsServiceModel> News { get; init; }
    }
}
