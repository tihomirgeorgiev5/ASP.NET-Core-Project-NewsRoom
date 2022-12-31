using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsRoom.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
