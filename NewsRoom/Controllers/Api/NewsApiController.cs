using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models.News;
using System.Collections;
using System.Linq;

namespace NewsRoom.Controllers.Api
{
    [ApiController]
    [Route("api/news")]
    public class NewsApiController : ControllerBase
    {
        private readonly NewsRoomDbContext data;

        public NewsApiController(NewsRoomDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery] AllNewsQueryModel query)
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

            return Ok(query);
        }



    }
}
