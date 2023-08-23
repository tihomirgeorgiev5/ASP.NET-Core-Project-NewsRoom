using Microsoft.AspNetCore.Mvc;
using NewsRoom.Models.FaqEntity;
using NewsRoom.Services.About;
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

        public async Task<IActionResult> Index()
        {
            var faq = await aboutService.GetAllFaqsAsync<FaqViewModel>();

            return View(faq);
        }
    }
}
