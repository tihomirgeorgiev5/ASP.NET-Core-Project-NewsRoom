using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Models.News;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsRoomDbContext data;

        public NewsController(NewsRoomDbContext data) => this.data = data;
        public IActionResult Add() => View(new AddNewsFormModel 
        {
            Categories = this.GetNewsCategories()
        });

        public IActionResult All(
            string area,
            string searchTerm,
            NewsSorting sorting)
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

            newsQuery = sorting switch
            {
                NewsSorting.DateCreated => newsQuery.OrderByDescending(n => n.Id),
                NewsSorting.AreaAndTitle => newsQuery.OrderBy(n => n.Area).ThenBy(n => n.Title),
                NewsSorting.Date or _ => newsQuery.OrderByDescending(n => n.Date),
                
            };

            var news = newsQuery
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

            return View(new AllNewsQueryModel
            {
                Area = area,
                Areas = newsAreas,
                SearchTerm = searchTerm,
                Sorting = sorting,
                News = news,
               
            });

        }

        [HttpPost]
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
