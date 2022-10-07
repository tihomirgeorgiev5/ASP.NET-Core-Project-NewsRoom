using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Models.News;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsRoomDbContext data;

        public NewsController(NewsRoomDbContext data) => this.data = data;
        

        public IActionResult All([FromQuery]AllNewsQueryModel query)
        {
            var newsQuery = this.data.News.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Area))
            {
                newsQuery = newsQuery.Where(n => n.Area == query.Area);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                newsQuery = newsQuery.Where(n =>
                 n.Area.ToLower().Contains(query.SearchTerm.ToLower()) ||
                n.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
                n.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            newsQuery = query.Sorting switch
            {
                NewsSorting.DateCreated => newsQuery.OrderByDescending(n => n.Id),
                NewsSorting.AreaAndTitle => newsQuery.OrderBy(n => n.Area).ThenBy(n => n.Title),
                NewsSorting.Date or _ => newsQuery.OrderByDescending(n => n.Date),
                
            };

            var totalNews = newsQuery.Count();

            var news = newsQuery
                .Skip((query.CurrentPage - 1) * AllNewsQueryModel.NewsPerPage)
                .Take(AllNewsQueryModel.NewsPerPage)
                .Select(n => new NewsListingViewModel
                {
                    Id = n.Id,
                    Area = n.Area,
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    Date = n.Date,
                    Category = n.Category.Name
                })
                .ToList();

            var newsAreas = this.data
                .News
                .Select(n => n.Area)
                .Distinct()
                .OrderBy(a => a)
                .ToList();

            query.TotalNews = totalNews;
            query.Areas = newsAreas;
            query.News = news;

            return View(query);

        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIsJournalist = this.data
                .Journalists
                .Any(j => j.UserId == userId);
            return View(new AddNewsFormModel
            {
                Categories = this.GetNewsCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add (AddNewsFormModel aNews)
        {
            if(!this.data.Categories.Any(n => n.Id == aNews.CategoryId))
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
            };

            this.data.News.Add(aNewsData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

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
