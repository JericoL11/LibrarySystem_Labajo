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
    public class DetailsController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public DetailsController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
            var librarySystem_LabajoContext = _context.Details.Include(d => d.FK_books_id).Include(d => d.FK_record_id).Include(d=> d.FK_record_id.FK_borrower);
            return View(await librarySystem_LabajoContext.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .Include(d => d.FK_books_id)
                .Include(d => d.FK_record_id)
                .FirstOrDefaultAsync(m => m.details_id == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
            Details details = new Details();
            //========= RECORDS  ==========
            // Fetching data from your database context
            var rec_data = _context.Records.Include(r => r.FK_borrower).Select(x => new {
                CombinedValue =$"{x.record_id} / {x.FK_borrower.b_fname} {x.FK_borrower.b_lname}",
                Id = x.record_id }).ToList(); // Assuming you have an Id property or some unique identifier


          


            // Creating a SelectList
            ViewData["RecordsName"] = new SelectList(rec_data, "Id", "CombinedValue");



            //default 
            ViewData["books_id"] = new SelectList(_context.Books.Where(b => b.bookId != details.books_id), "bookId", "bookName");


            /*  ViewData["record_id"] = new SelectList(_context.Records, "record_id", "record_id");*/
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("details_id,books_id,record_id,return_date")] Details details)
        {
            //checking the books if available
            bool book_data = _context.Details.Any(d => d.books_id == details.books_id);

            //========= BOOKS  ==========

            // Fetching data from your database context
            var rec_data = _context.Books.Where(b => b.bookId != details.books_id);




            if (string.IsNullOrEmpty(details.record_id.ToString()))
            {
                ModelState.AddModelError("", "Record Id is required");
                
                var rec_data2 = _context.Records.Include(r => r.FK_borrower).Select(x => new {
                    CombinedValue = $"{x.record_id} / {x.FK_borrower.b_fname} {x.FK_borrower.b_lname}",
                    Id = x.record_id
                }).ToList();

                // Creating a SelectList
                ViewData["RecordsName"] = new SelectList(rec_data2, "Id", "CombinedValue");


                ViewData["books_id"] = new SelectList(_context.Books/*.Where( b=> b.bookId != details.books_id)*/, "bookId", "bookName");

                return View();

            }

            else if (book_data == true)
            {
                ModelState.AddModelError("", "Book is not available");
             

                var rec_data2 = _context.Records.Include(r => r.FK_borrower).Select(x => new {
                    CombinedValue = $"{x.record_id} / {x.FK_borrower.b_fname} {x.FK_borrower.b_lname}",
                    Id = x.record_id
                }).ToList();

                // Creating a SelectList
                ViewData["RecordsName"] = new SelectList(rec_data2, "Id", "CombinedValue");


                ViewData["books_id"] = new SelectList(_context.Books/*.Where( b=> b.bookId != details.books_id)*/, "bookId", "bookName");

                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(details);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["books_id"] = new SelectList(_context.Books, "bookId", "author", details.books_id);
                ViewData["record_id"] = new SelectList(_context.Records, "record_id", "record_id", details.record_id);
                return View(details);
            }

           
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var details = await _context.Details.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }



            // Fetching data from your database context with condition
            var data = _context.Records.Include(r => r.FK_borrower).Where(r => r.record_id == details.record_id).Select(x => new {
                CombinedValue = x.record_id + " " + x.FK_borrower.b_fname + " " + x.FK_borrower.b_lname,
                Id = x.record_id // Assuming you have an Id property or some unique identifier
            }).ToList();

            // Creating a SelectList
            ViewData["RecordsList"] = new SelectList(data, "Id", "CombinedValue");


            ViewData["books_id"] = new SelectList(_context.Books.Where(b => b.bookId == details.books_id), "bookId", "bookName", details.books_id);
            /*
                        ViewData["record_id"] = new SelectList(_context.Records.Where(r => r.record_id == details.record_id), "record_id", "record_id", details.record_id);*/
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("details_id,books_id,record_id,return_date")] Details details)
        {



            if (id != details.details_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailsExists(details.details_id))
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
            ViewData["books_id"] = new SelectList(_context.Books, "bookId", "author", details.books_id);
            ViewData["record_id"] = new SelectList(_context.Records, "record_id", "record_id", details.record_id);
            return View(details);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var details = await _context.Details
                .Include(d => d.FK_books_id)
                .Include(d => d.FK_record_id)
                .FirstOrDefaultAsync(m => m.details_id == id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
       
              //detaials id and ID mathcing and returned date checking
            bool return_Details = _context.Details.Any(d => d.details_id == id && string.IsNullOrEmpty(d.return_date.ToString()));
  
          
            
            if(return_Details == true)
            {

                //default view
                var details_reload = await _context.Details
               .Include(d => d.FK_books_id)
               .Include(d => d.FK_record_id)
               .FirstOrDefaultAsync(m => m.details_id == id);

                ModelState.AddModelError("", "Book is not returned yet.");
                return View(details_reload);
            }
            else
            {

                if (_context.Details == null)
                {


                    return Problem("Entity set 'LibrarySystem_LabajoContext.Details'  is null.");
                }
                var details = await _context.Details.FindAsync(id);
                if (details != null)
                {
                    _context.Details.Remove(details);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        
        }

        private bool DetailsExists(int id)
        {
          return (_context.Details?.Any(e => e.details_id == id)).GetValueOrDefault();
        }
    }
}
