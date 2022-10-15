using NewsRoom.Data;
using NewsRoom.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Services.News
{
    public class NewsService : INewsService
    {
        private readonly NewsRoomDbContext data;
        public NewsService(NewsRoomDbContext data) 
            => this.data = data;
        

        public NewsQueryServiceModel All(
            string area,
            string searchTerm,
            NewsSorting newsSorting,
            int currentPage,
            int newsPerPage)
        {
            var newsQuery = this.data.News.AsQueryable();

            if (!string.IsNullOrWhiteSpace(area))
            {
                newsQuery = newsQuery.Where(n => n.Area == area);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                newsQuery = newsQuery.Where(n =>
                 n.Area.ToLower().Contains(searchTerm.ToLower()) ||
                n.Title.ToLower().Contains(searchTerm.ToLower()) ||
                n.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            newsQuery = newsSorting switch
            {
                NewsSorting.DateCreated => newsQuery.OrderByDescending(n => n.Id),
                NewsSorting.AreaAndTitle => newsQuery.OrderBy(n => n.Area).ThenBy(n => n.Title),
                NewsSorting.Date or _ => newsQuery.OrderByDescending(n => n.Date),

            };

            var totalNews = newsQuery.Count();

            var news = newsQuery
                .Skip((currentPage - 1) * newsPerPage)
                .Take(newsPerPage)
                .Select(n => new NewsServiceModel
                {
                    Id = n.Id,
                    Area = n.Area,
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    Date = n.Date,
                    Category = n.Category.Name
                })
                .ToList();

            return new NewsQueryServiceModel
            {
                TotalNews = totalNews,
                CurrentPage = currentPage,
                NewsPerPage = newsPerPage,
                News = news
            };

            
        }

        public IEnumerable<string> AllNewsAreas()
            => this.data
                .News
                .Select(n => n.Area)
                .Distinct()
                .OrderBy(a => a)
                .ToList();

    }
}
