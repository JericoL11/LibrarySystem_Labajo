using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem_Labajo.Data;
using LibrarySystem_Labajo.Models;

namespace LibrarySystem_Labajo.Controllers
{
    //done scaffolding
    public class BookCategoriesController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public BookCategoriesController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        // GET: BookCategories
        public async Task<IActionResult> Index()
        {
              return _context.BookCategory != null ? 
                          View(await _context.BookCategory.ToListAsync()) :
                          Problem("Entity set 'LibrarySystem_LabajoContext.BookCategory'  is null.");
        }

        // GET: BookCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookCategory == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategory
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        // GET: BookCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,categoryName")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookCategory);
        }

        // GET: BookCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookCategory == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategory.FindAsync(id);
            if (bookCategory == null)
            {
                return NotFound();
            }
            return View(bookCategory);
        }

        // POST: BookCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryId,categoryName")] BookCategory bookCategory)
        {
            if (id != bookCategory.categoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryExists(bookCategory.categoryId))
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
            return View(bookCategory);
        }

        // GET: BookCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookCategory == null)
            {
                return NotFound();
            }

            var bookCategory = await _context.BookCategory
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return View(bookCategory);
        }

        // POST: BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookCategory == null)
            {
                return Problem("Entity set 'LibrarySystem_LabajoContext.BookCategory'  is null.");
            }
            var bookCategory = await _context.BookCategory.FindAsync(id);
            if (bookCategory != null)
            {
                _context.BookCategory.Remove(bookCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCategoryExists(int id)
        {
          return (_context.BookCategory?.Any(e => e.categoryId == id)).GetValueOrDefault();
        }
    }
}
