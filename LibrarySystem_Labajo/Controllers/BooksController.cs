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
    public class BooksController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public BooksController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var librarySystem_LabajoContext = _context.Books.Include(b => b.bookcategory);
            return View(await librarySystem_LabajoContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.bookcategory)
                .FirstOrDefaultAsync(m => m.bookId == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
          
            //category select display
            ViewData["categoryId"] = new SelectList(_context.Set<BookCategory>(), "categoryId", "categoryName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bookId,bookName,author,categoryId,dateAdded")] Books books)
        {
            //check book name
            var check_bookName = _context.Books.FirstOrDefault( b => b.bookName == books.bookName);

            if(check_bookName != null)
            {
                ModelState.AddModelError("", "Book name is already exist.");
                //category select display
                ViewData["categoryId"] = new SelectList(_context.Set<BookCategory>(), "categoryId", "categoryName");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(books);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["categoryId"] = new SelectList(_context.Set<BookCategory>(), "categoryId", "categoryId", books.categoryId);
                return View(books);
            }
           
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            ViewData["categoryId"] = new SelectList(_context.Set<BookCategory>(), "categoryId", "categoryId", books.categoryId);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("bookId,bookName,author,categoryId,dateAdded")] Books books)
        {
            if (id != books.bookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.bookId))
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
            ViewData["categoryId"] = new SelectList(_context.Set<BookCategory>(), "categoryId", "categoryId", books.categoryId);
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.bookcategory)
                .FirstOrDefaultAsync(m => m.bookId == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibrarySystem_LabajoContext.Books'  is null.");
            }
            var books = await _context.Books.FindAsync(id);
            if (books != null)
            {
                _context.Books.Remove(books);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
          return (_context.Books?.Any(e => e.bookId == id)).GetValueOrDefault();
        }
    }
}
