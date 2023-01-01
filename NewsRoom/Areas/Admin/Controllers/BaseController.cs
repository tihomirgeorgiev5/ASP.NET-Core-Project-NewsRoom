using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static NewsRoom.Areas.Admin.AdminConstants;

namespace NewsRoom.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class BaseController : Controller
    {
    }
}
 