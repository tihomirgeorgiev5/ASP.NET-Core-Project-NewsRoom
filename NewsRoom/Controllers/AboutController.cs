using Microsoft.AspNetCore.Mvc;
using NewsRoom.Areas.Admin.Models;
using NewsRoom.Models.FaqEntity;
using NewsRoom.Services.About;
using System.Linq;
using System.Threading.Tasks;

namespace NewsRoom.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService aboutService;

        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FaqCreateInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            await this.aboutService.CreateAsync(model);
            return this.RedirectToAction("All", "About", new { area = "Admin" });
        }

        [HttpGet("Admin/About/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var faqToEdit = await this.aboutService.GetByIdAsync<FaqEditViewModel>(id);
            var _model = new FaqEditViewModel()
            {
                Question = faqToEdit.Question,
                Answer = faqToEdit.Answer,
            };
            return View(_model);
        }

        [HttpPost("Admin/About/Edit")]
        public async Task<IActionResult> Edit(FaqEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                return this.View(model);
            }
            await this.aboutService.EditAsync(model);
            return RedirectToAction("All", "About", new { area = "Admin" });
        }

        public async Task<IActionResult> All()
        {
            var faq = await this.aboutService.GetAllFaqsAsync<FaqViewModel>();
            ViewBag.faq = faq.ToList();
            return View();
        }

        public async Task<IActionResult> Delete(int faqId)
        {
            this.aboutService.DeleteById(faqId);
            return RedirectToAction(nameof(All));
        }
    }
}
