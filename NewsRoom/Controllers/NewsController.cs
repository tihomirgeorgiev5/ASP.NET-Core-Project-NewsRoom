using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
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
        private readonly IMapper mapper;
  
        public NewsController(
            INewsService news,
            IJournalistService journalists,
            IMapper mapper)
        {
            this.news = news;
            this.journalists = journalists;
            this.mapper = mapper;
             
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
            var myNews = this.news.ByUser(this.User.Id());

            return View(myNews);
        }

        public IActionResult Details(int id, string information)
        {
            var aNews = this.news.Details(id);

            if (information != aNews.ToFriendlyUrl())
            {
                return BadRequest();
            }

            return View(aNews);
        }

        [Authorize]
        public IActionResult Add()
        {
            

            if (!this.journalists.IsJournalist(this.User.Id()))
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
            var journalistId = this.journalists.IdByUser(this.User.Id());

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

            var newsId = this.news.Create(
               aNews.Area ,
               aNews.Title,
               aNews.Description,
               aNews.ImageUrl ,
               aNews.Date ,
               aNews.CategoryId ,
               journalistId 
               );

            return RedirectToAction(nameof(Details), new { id = newsId, information = aNews.ToFriendlyUrl()});
        }

        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();
            if (!this.journalists.IsJournalist(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(JournalistsController.Become), "Journalists");
            }

            var aNews = this.news.Details(id);

            if (aNews.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var aNewsForm = this.mapper.Map<NewsFormModel>(aNews);

            aNewsForm.Categories = this.news.AllCategories();



            return View(aNewsForm);

        }

        [HttpPost]
        [Authorize]

        public IActionResult Edit(int id, NewsFormModel aNews)
        {
            var journalistId = this.journalists.IdByUser(this.User.Id());

            if (journalistId == 0 && !User.IsAdmin())
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

            if (!this.news.IsByJournalist(id, journalistId) && !User.IsAdmin())
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
               aNews.CategoryId,
               this.User.IsAdmin()
               );

            ///TempData[GlobalMessageKey] = "Your news was edited!";


            return RedirectToAction(nameof(Details), new { id, information = aNews.ToFriendlyUrl() });

        }






    }
}
