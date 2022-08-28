using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
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
            return View();
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
