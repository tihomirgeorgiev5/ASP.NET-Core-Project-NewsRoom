using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsRoom.Models.News
{
    public class AllNewsQueryModel
    {
        public string Area { get; init; }
        

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public NewsSorting Sorting { get; init; }

        public IEnumerable<string> Areas { get; set; }
        public  IEnumerable<NewsListingViewModel> News { get; set; }
    }
}
