using Microsoft.AspNetCore.Mvc;

namespace Asp_Mvc.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
