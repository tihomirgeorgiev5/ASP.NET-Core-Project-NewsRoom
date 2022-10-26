using NewsRoom.Models;
using System;
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

        int Create(
            string area,
            string title,
            string description,
            string imageUrl,
            DateTime date,
            int categoryId,
            int journalistId
            );

        bool Edit(
            int id,
            string area,
            string title,
            string description,
            string imageUrl,
            DateTime date,
            int categoryId,
            int journalistId
            );

        IEnumerable<NewsServiceModel> ByUser(string userId);

        IEnumerable<string> AllAreas();
        IEnumerable<NewsCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
