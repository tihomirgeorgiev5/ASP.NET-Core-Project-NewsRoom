using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure.Extensions;
using NewsRoom.Models.Journalists;
using System.Linq;

using static NewsRoom.WebConstants;

namespace NewsRoom.Controllers
{
    public class JournalistsController : Controller
    {
        private readonly NewsRoomDbContext data;

        public JournalistsController(NewsRoomDbContext data)
            => this.data = data;


        [Authorize]
        public IActionResult Become()
            => View();

        [HttpPost]
        [Authorize]

        public IActionResult Become(BecomeJournalistFormModel journalist)
        {
            var userId = this.User.Id();

            var userIsAlreadyJournalist = this.data
                .Journalists
                .Any(j => j.UserId == userId);

            if (userIsAlreadyJournalist)

            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(journalist);
            }

            var journalistData = new Journalist
            {
                Name = journalist.Name,
                PhoneNumber = journalist.PhoneNumber,
                UserId = userId
            };

            this.data.Journalists.Add(journalistData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becoming a journalist!";

            return RedirectToAction(nameof(NewsController.All), "News");

        }
    }
}
            


