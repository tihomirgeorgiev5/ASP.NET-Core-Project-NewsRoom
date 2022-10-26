using NewsRoom.Models;
using System.Collections.Generic;

namespace NewsRoom.Services.News
{
   public interface INewsService
    {
        NewsQueryServiceModel All(
            string area,
            string searchTerm,
            NewsSorting newsSorting,
            int currentPage,
            int newsPerPage
            );

        NewsDetailsServiceModel Details(int id);

        IEnumerable<NewsServiceModel> ByUser(string userId);

        IEnumerable<string> AllAreas();
        IEnumerable<NewsCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
