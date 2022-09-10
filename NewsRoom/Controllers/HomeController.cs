using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models;
using NewsRoom.Models.News;
using System.Diagnostics;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsRoomDbContext data;

        public HomeController(NewsRoomDbContext data) => this.data = data;
        public IActionResult Index()
        {
            var news = this.data
               .News
               .OrderByDescending(n => n.Id)
               .Select(n => new NewsListingViewModel
               {
                   Id = n.Id,
                   Area = n.Area,
                   Title = n.Title,
                   ImageUrl = n.ImageUrl,
                   Date = n.Date,
                   Category = n.Category.Name
               })
               .Take(3)
               .ToList();

            return View(news);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
           View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
