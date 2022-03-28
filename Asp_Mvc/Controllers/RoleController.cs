using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Mvc.Controllers
{
    [Authorize(Policy = "Admins")]
    //[Authorize(Roles = "admin,user")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
