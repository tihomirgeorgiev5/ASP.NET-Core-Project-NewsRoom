using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure;
using NewsRoom.Models;
using NewsRoom.Models.News;
using NewsRoom.Services.News;
using System.Collections.Generic;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly NewsRoomDbContext data;

        public NewsController(INewsService news, NewsRoomDbContext data)
        {
            this.news = news;
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

            var newsAreas = this.news.AllNewsAreas();

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
            

            if (!this.UserIsDealer())
            {
                

                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }

            return View(new AddNewsFormModel
            {
                Categories = this.GetNewsCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add (AddNewsFormModel aNews)
        {
            var journalistId = this.data
                .Journalists
                .Where(j => j.UserId == this.User.GetId())
                .Select(j => j.Id)
                .FirstOrDefault();

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
                aNews.Categories = this.GetNewsCategories();
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
         
        private bool UserIsDealer()
            => this.data
                .Journalists
                .Any(j => j.UserId == this.User.GetId());

        private IEnumerable<NewsCategoryViewModel> GetNewsCategories() =>
            this.data
            .Categories
            .Select(n => new NewsCategoryViewModel
            {
                Id = n.Id,
                Name = n.Name,
            })
            .ToList();
       
    }
}
