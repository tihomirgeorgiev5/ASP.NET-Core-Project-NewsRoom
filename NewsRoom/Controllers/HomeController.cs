using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models;
using NewsRoom.Models.Home;
using NewsRoom.Services.Statistics;
using System.Diagnostics;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;
        private readonly NewsRoomDbContext data;

        public HomeController(
            IStatisticsService statistics,
             IMapper mapper,
            NewsRoomDbContext data)
        {
            this.statistics = statistics;
            this.mapper = mapper;
            this.data = data;
            
        }

        public IActionResult Index()
        {
            var news = this.data
               .News
               .OrderByDescending(n => n.Id)
               .ProjectTo<NewsIndexViewModel>(this.mapper.ConfigurationProvider)
               .Take(3)
               .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel 
            { 
                TotalNews = totalStatistics.TotalNews,
                TotalReaders = totalStatistics.TotalReaders,
                News = news,
            });
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
           View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
