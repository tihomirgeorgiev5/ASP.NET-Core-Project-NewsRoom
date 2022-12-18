using Microsoft.AspNetCore.Mvc;
using NewsRoom.Services.News;

namespace NewsRoom.Areas.Admin.Controllers
{
    
    public class NewsController : AdminController
    {
        private readonly INewsService news;

        public NewsController(INewsService news) => this.news = news;
        public IActionResult All() => View(this.news.All().News);

    }
}
