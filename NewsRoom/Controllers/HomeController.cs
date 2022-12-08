using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models.Home;
using NewsRoom.Services.News.Models;
using NewsRoom.Services.Statistics;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly AutoMapper.IConfigurationProvider mapper;
        private readonly NewsRoomDbContext data;

        public HomeController(
            IStatisticsService statistics,
             IMapper mapper,
            NewsRoomDbContext data)
        {
            this.statistics = statistics;
            this.mapper = mapper.ConfigurationProvider;
            this.data = data;
            
        }

        public IActionResult Index()
        {
            var news = this.data
               .News
               .OrderByDescending(n => n.Id)
               .ProjectTo<LatestNewsServiceModel>(this.mapper)
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
        
       
        public IActionResult Error() =>  View();
        
    }
}
