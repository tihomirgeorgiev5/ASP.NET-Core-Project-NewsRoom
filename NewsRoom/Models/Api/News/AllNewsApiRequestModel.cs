using NewsRoom.Models.News;

namespace NewsRoom.Models.Api.News
{
    public class AllNewsApiRequestModel
    {
        public string Area { get; init; }


        
        public string SearchTerm { get; init; }

        public NewsSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalNews { get; set; }
    }
}
