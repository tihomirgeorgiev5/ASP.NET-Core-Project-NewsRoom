using System.Collections.Generic;

namespace NewsRoom.Models.Api.News
{
    public class AllNewsApiResponseModel
    {
        public int CurrentPage { get; init; }

        public int TotalNews { get; set; }

        public IEnumerable<NewsResponseModel> News { get; init; }
    }
}
