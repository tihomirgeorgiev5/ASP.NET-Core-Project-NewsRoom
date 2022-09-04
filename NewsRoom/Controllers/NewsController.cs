using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Models.News;
using System.Collections.Generic;
using System.Linq;

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

            return RedirectToAction("Index", "Home");
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
