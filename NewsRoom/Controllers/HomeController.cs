using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsRoom.Models;
using System.Diagnostics;

namespace NewsRoom.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index() =>  View();
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
           View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
