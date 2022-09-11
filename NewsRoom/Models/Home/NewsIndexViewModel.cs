using System;

namespace NewsRoom.Models.Home
{
    public class NewsIndexViewModel
    {
        public int Id { get; init; }
        public string Area { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

    }
}
