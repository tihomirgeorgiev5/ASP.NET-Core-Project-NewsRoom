using NewsRoom.Models;
using NewsRoom.Services.News.Models;
using System;
using System.Collections.Generic;

namespace NewsRoom.Services.News
{
    public interface INewsService
    {
        NewsQueryServiceModel All(
            string area = null,
            string searchTerm = null,
            NewsSorting newsSorting = NewsSorting.DateCreated,
            int currentPage = 1,
            int newsPerPage = int.MaxValue,
            bool publicOnly = true
            );

        IEnumerable<LatestNewsServiceModel> Latest();

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
