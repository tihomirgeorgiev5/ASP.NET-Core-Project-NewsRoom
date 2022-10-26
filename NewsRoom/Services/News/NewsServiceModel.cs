using System;

namespace NewsRoom.Services.News
{
    public class NewsServiceModel
    {
       
        public int Id { get; init; }
        public string Area { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

        public string CategoryName { get; init; }
    }
}
