using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure;
using NewsRoom.Models.News;
using NewsRoom.Services.Journalists;
using NewsRoom.Services.News;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly IJournalistService journalists;
        private readonly NewsRoomDbContext data;

        public NewsController(INewsService news, IJournalistService journalists, NewsRoomDbContext data)
        {
            this.news = news;
            this.journalists = journalists;
            this.data = data;
            
        }


        public IActionResult All([FromQuery] AllNewsQueryModel query)
        {
            var queryResult = this.news.All(
                query.Area,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllNewsQueryModel.NewsPerPage);

            var newsAreas = this.news.AllAreas();

            query.TotalNews = queryResult.TotalNews;
            query.Areas = newsAreas;
            query.News = queryResult.News;

            return View(query);

        }

        [Authorize]
        public IActionResult Mine()
        {
            var myNews = this.news.ByUser(this.User.GetId());

            return View(myNews);
        }

        [Authorize]
        public IActionResult Add()
        {
            

            if (!this.journalists.IsJournalist(this.User.GetId()))
            {
                

                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }

            return View(new NewsFormModel
            {
                Categories = this.news.AllCategories()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add (NewsFormModel aNews)
        {
            var journalistId = this.journalists.GetIdByUser(this.User.GetId());

            if (journalistId == 0)
            {
                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }
            if (!this.data.Categories.Any(n => n.Id == aNews.CategoryId))
            {
                this.ModelState.AddModelError(nameof(aNews.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                aNews.Categories = this.news.AllCategories();
                return View(aNews);
            }

            var aNewsData = new ANews
            {
                Area = aNews.Area,
                Title = aNews.Title,
                Description = aNews.Description,
                ImageUrl = aNews.ImageUrl,
                Date = aNews.Date,
                CategoryId = aNews.CategoryId,
                JournalistId = journalistId,
            };

            this.data.News.Add(aNewsData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
   
      
       
    }
}
