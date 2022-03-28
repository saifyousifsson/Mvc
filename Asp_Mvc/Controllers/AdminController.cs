using Asp_Mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Mvc.Controllers
{   
  
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Users()
        {
            return View(await _context.UserAddresses.Include(x=>x.User).Include(x=>x.Address).ToListAsync());
        }
    }
}
