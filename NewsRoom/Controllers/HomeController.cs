using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsRoom.Services.News;
using NewsRoom.Services.News.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using static NewsRoom.WebConstants.Cache;

namespace NewsRoom.Controllers
{
    public class HomeController : BaseController
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
           
            var latestNews = this.cache.Get<List<LatestNewsServiceModel>>(LatestNewsCacheKey);

            if (latestNews == null)
            {
                latestNews = this.news
                .Latest()
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestNewsCacheKey, latestNews, cacheOptions);
            }

            return View(latestNews);
        }
        
       
        public IActionResult Error() =>  View();

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddYears(1),
                    SameSite = SameSiteMode.Strict
                });

            return LocalRedirect(returnUrl);
            
        }
        
    }
}
