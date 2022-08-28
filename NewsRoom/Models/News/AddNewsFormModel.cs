using System;

namespace NewsRoom.Models.News
{
    public class AddNewsFormModel
    {
     
        public string Area { get; init; }
     
        public string Title { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public DateTime Date { get; init; }

        public int CategoryId { get; init; }
    }
}
