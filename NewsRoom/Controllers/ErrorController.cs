using Microsoft.AspNetCore.Mvc;
using NewsRoom.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace NewsRoom.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/404")]
        [ResponseCache(Duration =0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error404()
        {
            var errorModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.Status404NotFound,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };
            return View(errorModel);
        }

        [Route("/Error/500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error500()
        {
            var errorModel = new ErrorViewModel
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };
            return View(errorModel);
        }
    }
}
