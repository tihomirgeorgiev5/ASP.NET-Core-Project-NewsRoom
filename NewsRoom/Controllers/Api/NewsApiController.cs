using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Models;
using NewsRoom.Models.Api.News;
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

        [HttpGet]
        public ActionResult<AllNewsApiResponseModel> All([FromQuery] AllNewsApiRequestModel query)
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
                .Skip((query.CurrentPage - 1) * query.NewsPerPage)
                .Take(query.NewsPerPage)
                .Select(n => new NewsResponseModel
                {
                    Id = n.Id,
                    Area = n.Area,
                    Title = n.Title,
                    ImageUrl = n.ImageUrl,
                    Date = n.Date,
                    Category = n.Category.Name
                })
                .ToList();

            return new AllNewsApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                TotalNews = totalNews,
                News = news,
            };

           
        }



    }
}
