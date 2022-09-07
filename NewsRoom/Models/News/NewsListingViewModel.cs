using System;

namespace NewsRoom.Models.News
{
    public class NewsListingViewModel
    {
        public int Id { get; init; }
        public string Area { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

        public string Category { get; init; }
    }
}
