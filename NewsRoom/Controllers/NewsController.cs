using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Infrastructure;
using NewsRoom.Models.News;
using NewsRoom.Services.Journalists;
using NewsRoom.Services.News;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly IJournalistService journalists;
  
        public NewsController(INewsService news, IJournalistService journalists)
        {
            this.news = news;
            this.journalists = journalists;
             
        }


        public IActionResult All([FromQuery] AllNewsQueryModel query)
        {
            var queryResult = this.news.All(
                query.Area,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllNewsQueryModel.NewsPerPage);

            var newsAreas = this.news.AllAreas();

            query.TotalNews = queryResult.TotalNews;
            query.Areas = newsAreas;
            query.News = queryResult.News;

            return View(query);

        }

        [Authorize]
        public IActionResult Mine()
        {
            var myNews = this.news.ByUser(this.User.GetId());

            return View(myNews);
        }

        [Authorize]
        public IActionResult Add()
        {
            

            if (!this.journalists.IsJournalist(this.User.GetId()))
            {
                

                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }

            return View(new NewsFormModel
            {
                Categories = this.news.AllCategories()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add (NewsFormModel aNews)
        {
            var journalistId = this.journalists.IdByUser(this.User.GetId());

            if (journalistId == 0)
            {
                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }
            if (!this.news.CategoryExists(aNews.CategoryId))
            {
                this.ModelState.AddModelError(nameof(aNews.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                aNews.Categories = this.news.AllCategories();
                return View(aNews);
            }

            this.news.Create(
               aNews.Area ,
               aNews.Title,
               aNews.Description,
               aNews.ImageUrl ,
               aNews.Date ,
               aNews.CategoryId ,
               journalistId 
               );

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();
            if (!this.journalists.IsJournalist(userId))
            {
                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }

            var aNews = this.news.Details(id);

            if (aNews.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new NewsFormModel
            {
                Area = aNews.Area,
                Title = aNews.Title,
                Description = aNews.Description,
                ImageUrl = aNews.ImageUrl,
                Date = aNews.Date,
                CategoryId = aNews.CategoryId,
                Categories = this.news.AllCategories()
            });

        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(int id, NewsFormModel aNews)
        {
            var journalistId = this.journalists.IdByUser(this.User.GetId());

            if (journalistId == 0)
            {
                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }
            if (!this.news.CategoryExists(aNews.CategoryId))
            {
                this.ModelState.AddModelError(nameof(aNews.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                aNews.Categories = this.news.AllCategories();
                return View(aNews);
            }

            if (!this.news.IsByJournalist(id, journalistId))
            {
                return BadRequest();
            }

            this.news.Edit(
                id,
               aNews.Area,
               aNews.Title,
               aNews.Description,
               aNews.ImageUrl,
               aNews.Date,
               aNews.CategoryId
               );


            return RedirectToAction(nameof(All));

        }






    }
}
