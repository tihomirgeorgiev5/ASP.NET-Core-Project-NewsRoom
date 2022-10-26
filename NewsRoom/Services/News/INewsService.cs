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

        NewsDetailsServiceModel Details(int aNewsId);

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
            int aNewsId,
            string area,
            string title,
            string description,
            string imageUrl,
            DateTime date,
            int categoryId
            );

        IEnumerable<NewsServiceModel> ByUser(string userId);

        bool IsByJournalist(int aNewsId, int journalistId);

        IEnumerable<string> AllAreas();
        IEnumerable<NewsCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
