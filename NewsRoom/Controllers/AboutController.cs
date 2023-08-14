using Microsoft.AspNetCore.Mvc;

namespace NewsRoom.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
