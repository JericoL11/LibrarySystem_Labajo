using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem_Labajo.Data;
using LibrarySystem_Labajo.Models;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem_Labajo.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public UsersController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            //Assigning of Viewbag from Sesstion that setted in LoginUser
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();


            return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'LibrarySystem_LabajoContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Username,Password,confirm_Password,BirthDate")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Username,Password,confirm_Password,BirthDate")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'LibrarySystem_LabajoContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return RedirectToAction(nameof(Index));

        }

        private bool UserExists(int id)
        {
            ViewBag.sessionName = HttpContext.Session.GetString("Name").ToUpper();
            return (_context.User?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
