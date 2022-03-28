using Microsoft.AspNetCore.Mvc;

namespace Asp_Mvc.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
