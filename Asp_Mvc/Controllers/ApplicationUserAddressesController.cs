#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp_Mvc.Data;
using Asp_Mvc.Models;

namespace Asp_Mvc.Controllers
{
    public class ApplicationUserAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserAddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationUserAddresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserAddresses.Include(a => a.Address).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApplicationUserAddresses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserAddress = await _context.UserAddresses
                .Include(a => a.Address)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserAddress == null)
            {
                return NotFound();
            }

            return View(Index);
        }

        // GET: ApplicationUserAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "AddressLine");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", "FirstName", "LastName");
            return View();
        }

        // POST: ApplicationUserAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User,Address")] ApplicationUserAddress applicationUserAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUserAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Address"] = new SelectList(_context.Addresses, "Id", "AddressLine", applicationUserAddress.AddressId);
            ViewData["User"] = new SelectList(_context.Users, "Id", "Id", applicationUserAddress.UserId);
            return View();
        }

        // GET: ApplicationUserAddresses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return View();
            }

            var applicationUserAddress = await _context.UserAddresses.FindAsync(id);
            if (applicationUserAddress == null)
            {
                return View();
            }
            ViewData["Address"] = new SelectList(_context.Addresses, "Id", "AddressLine", applicationUserAddress.AddressId);
            ViewData["User"] = new SelectList(_context.Users, "Id", "Id",  applicationUserAddress.UserId);
            return View();
        }

        // POST: ApplicationUserAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("User,Address")] ApplicationUserAddress applicationUserAddress, ApplicationUserAddress applicationUser)
        {
            if (id != applicationUserAddress.UserId)
            {
                return View();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(applicationUserAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserAddressExists(applicationUserAddress.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Address"] = new SelectList(_context.Addresses, "Id", "AddressLine", applicationUserAddress.AddressId);
            ViewData["User"] = new SelectList(_context.Users, "Id", "Id",  applicationUserAddress.UserId);
            return View();
        }
        // GET: ApplicationUserAddresses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUserAddress = await _context.UserAddresses
                .Include(a => a.Address)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (applicationUserAddress == null)
            {
                return NotFound();
            }

            return View(applicationUserAddress);
        }

        // POST: ApplicationUserAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUserAddress = await _context.UserAddresses.FindAsync(id);
            _context.UserAddresses.Remove(applicationUserAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserAddressExists(string id)
        {
            return _context.UserAddresses.Any(e => e.UserId == id);
        }
    }
}
