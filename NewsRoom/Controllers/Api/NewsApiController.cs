using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
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

        [HttpGet]
        public IEnumerable GetNews()
        {
            return this.data.News.ToList();
        }
    }
}
