using System.Collections.Generic;

namespace NewsRoom.Models.News
{
    public class AllNewsQueryModel
    {
        public IEnumerable<string> Areas { get; init; }

        public IEnumerable<string> SearchTerm { get; init; }

        public NewsSorting Sorting { get; init; }
        public  IEnumerable<NewsListingViewModel> News { get; init; }
    }
}
