using Microsoft.AspNetCore.Mvc;

namespace NewsRoom.Areas.Admin.Controllers
{
    
    public class NewsController : AdminController
    {
        public IActionResult Index() => View();

    }
}
