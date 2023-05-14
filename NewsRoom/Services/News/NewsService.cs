using AutoMapper;
using AutoMapper.QueryableExtensions;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Models;
using NewsRoom.Services.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Services.News 
{ 
    public class NewsService : INewsService
    {
        private readonly NewsRoomDbContext data;
        private readonly IConfigurationProvider mapper;
        public NewsService(
            NewsRoomDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }


        public NewsQueryServiceModel All(
            string area = null,
            string searchTerm = null,
            NewsSorting newsSorting = NewsSorting.DateCreated,
            int currentPage = 1,
            int newsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var newsQuery = this.data.News
                .Where(n => !publicOnly || n.IsPublic);

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

            var news = GetNews(
                newsQuery
                .Skip((currentPage - 1) * newsPerPage)
                .Take(newsPerPage));


            return new NewsQueryServiceModel
            {
                TotalNews = totalNews,
                CurrentPage = currentPage,
                NewsPerPage = newsPerPage,
                News = news
            };


        }

        public IEnumerable<LatestNewsServiceModel> Latest()
          => this.data
             .News
             .Where(n => n.IsPublic)
             .OrderByDescending(n => n.Id)
             .ProjectTo<LatestNewsServiceModel>(this.mapper)
             .Take(3)
             .ToList();

        public NewsDetailsServiceModel Details(int id)
        => this.data
            .News
            .Where(n => n.Id == id)
            .ProjectTo<NewsDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public int Create(string area, string title, string description, string imageUrl, DateTime date, int categoryId, int journalistId)
        {
            var aNewsData = new Data.Models.News
            {
                Area = area,
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                Date = date,
                CategoryId = categoryId,
                JournalistId = journalistId,
                IsPublic = false
            };

            this.data.News.Add(aNewsData);
            this.data.SaveChanges();

            return aNewsData.Id;
        }

        public bool Edit(
            int id,
            string area,
            string title,
            string description,
            string imageUrl,
            DateTime date,
            int categoryId,
            bool isPublic)
        {
            var aNewsData = this.data.News.Find(id);

            if (aNewsData == null)
            {
                return false;
            }

            

            aNewsData.Area = area;
            aNewsData.Title = title;
            aNewsData.Description = description;
            aNewsData.ImageUrl = imageUrl;
            aNewsData.Date = date;
            aNewsData.CategoryId = categoryId;
            aNewsData.IsPublic = isPublic;
                   
            this.data.SaveChanges();

            return true;

            
        }

        public IEnumerable<NewsServiceModel> ByUser(string userId)
            => GetNews(this.data
                .News
                .Where(n => n.Journalist.UserId == userId));

        public bool IsByJournalist(int aNewsId, int journalistId)
        => this.data
            .News
            .Any(n => n.Id == aNewsId && n.JournalistId == journalistId);

        public void ChangeVisibility(int newsId)
        {
            var aNews = this.data.News.Find(newsId);

            aNews.IsPublic = !aNews.IsPublic;

            this.data.SaveChanges();
        }

        public IEnumerable<string> AllAreas()
            => this.data
                .News
                .Select(n => n.Area)
                .Distinct()
                .OrderBy(a => a)
                .ToList();

        public IEnumerable<NewsCategoryServiceModel> AllCategories()
              => this.data
         .Categories
        .ProjectTo<NewsCategoryServiceModel>(this.mapper)
         .ToList();

        public bool CategoryExists(int categoryId)
        => this.data
            .Categories
            .Any(n => n.Id == categoryId);

        private IEnumerable<NewsServiceModel> GetNews(IQueryable<Data.Models.News> NewsQuery)
            => NewsQuery
            .ProjectTo<NewsServiceModel>(this.mapper)
                .ToList();

      
    }
}
