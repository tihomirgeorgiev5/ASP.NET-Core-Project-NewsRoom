using System;

namespace NewsRoom.Services.News.Models
{
    public class NewsServiceModel : INewsModel
    {
       
        public int Id { get; init; }
        public string Area { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

        public string CategoryName { get; init; }

        public bool IsPublic { get; init; }
    }
}
