using Microsoft.AspNetCore.Mvc;
using NewsRoom.Models.Home;
using NewsRoom.Services.News;
using NewsRoom.Services.Statistics;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService news;
        private readonly IStatisticsService statistics;

        

        public HomeController(
            INewsService news,
            IStatisticsService statistics)
        {
            this.news = news;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var latestNews = this.news
                .Latest()
                .ToList();

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
