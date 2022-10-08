using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsRoom.Data;
using NewsRoom.Data.Models;
using NewsRoom.Infrastructure;
using NewsRoom.Models.Journalists;
using System.Linq;

namespace NewsRoom.Controllers
{
    public class JournalistsController : Controller
    {
        private readonly NewsRoomDbContext data;

        public JournalistsController(NewsRoomDbContext data)
            => this.data = data;


        [Authorize]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize]

        public IActionResult Create(BecomeJournalistFormModel journalist)
        {
            var userId = this.User.GetId();

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

            return RedirectToAction("All", "News");

        }
    }
}
            


