using Microsoft.AspNetCore.Mvc;
using NewsRoom.Models.Api.News;
using NewsRoom.Services.News;
using NewsRoom.Services.News.Models;

namespace NewsRoom.Controllers.Api
{
    [ApiController]
    [Route("api/news")]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService news ;

        public NewsApiController(INewsService news) 
            => this.news = news;
        

       
        [HttpGet]
        public NewsQueryServiceModel All([FromQuery] AllNewsApiRequestModel query)
        {
            return this.news.All(
                query.Area,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.NewsPerPage
                );
         

           
        }



    }
}
