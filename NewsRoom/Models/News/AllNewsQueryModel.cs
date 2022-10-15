using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsRoom.Models.News
{
    public class AllNewsQueryModel
    {
        public const int NewsPerPage = 3;
        public string Area { get; init; }
        

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public NewsSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1; 

        public int TotalNews { get; set; }

        public IEnumerable<string> Areas { get; set; }
        public  IEnumerable<NewsListingViewModel> News { get; set; }
    }
}
