using Microsoft.AspNetCore.Mvc;
using NewsRoom.Services.News;

namespace NewsRoom.Areas.Admin.Controllers
{
    
    public class NewsController : AdminController
    {
        private readonly INewsService news;

        public NewsController(INewsService news) => this.news = news;
        public IActionResult All()
        {
            var news = this.news
                .All(publicOnly: false)
                .News;

            return View(news);
        }

    }
}
