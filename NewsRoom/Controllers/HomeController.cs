using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsRoom.Models.Home;
using NewsRoom.Services.News;
using NewsRoom.Services.News.Models;
using NewsRoom.Services.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService news;
        private readonly IStatisticsService statistics;
        private readonly IMemoryCache cache;

        

        public HomeController(
            INewsService news,
            IStatisticsService statistics,
            IMemoryCache cache)
        {
            this.news = news;
            this.statistics = statistics;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestNewsCacheKey = "LatestNewsCacheKey";

            var latestNews = this.cache.Get<List<LatestNewsServiceModel>>(latestNewsCacheKey);

            if (latestNews == null)
            {
                latestNews = this.news
                .Latest()
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestNewsCacheKey, latestNews, cacheOptions);
            }
            

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel 
            { 
                TotalNews = totalStatistics.TotalNews,
                TotalReaders = totalStatistics.TotalReaders,
                News = latestNews
            });
        }
        
       
        public IActionResult Error() =>  View();
        
    }
}
