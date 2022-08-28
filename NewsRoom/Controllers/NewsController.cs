using Microsoft.AspNetCore.Mvc;
using NewsRoom.Models.News;

namespace NewsRoom.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]

        public IActionResult Add (AddNewsFormModel aNews)
        {
            return View();
        }
    }
}
