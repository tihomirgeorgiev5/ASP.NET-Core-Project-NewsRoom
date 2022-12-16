﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsRoom.Services.News;
using NewsRoom.Services.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService news;
        private readonly IMemoryCache cache;

        

        public HomeController(
            INewsService news,
            IMemoryCache cache)
        {
            this.news = news;
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

            return View(latestNews);
        }
        
       
        public IActionResult Error() =>  View();
        
    }
}
