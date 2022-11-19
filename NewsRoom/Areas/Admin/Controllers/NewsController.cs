using Microsoft.AspNetCore.Mvc;
using static NewsRoom.Areas.Admin.AdminConstants;
 
namespace NewsRoom.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
