using System.Collections.Generic;

namespace NewsRoom.Models.Api.News
{
    public class AllNewsApiResponseModel
    {
        public int CurrentPage { get; init; }

        public int NewsPerPage { get; init; }

        public int TotalNews { get; init; }

        public IEnumerable<NewsResponseModel> News { get; init; }
    }
}
