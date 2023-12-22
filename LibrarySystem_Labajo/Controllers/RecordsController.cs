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
    public class RecordsController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public RecordsController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        // GET: Records
        public async Task<IActionResult> Index()
        {
            //Fetch all the data in FK
            var librarySystem_LabajoContext = _context.Records.Include(r => r.FK_borrower).Include(r => r.FK_librarian);
            return View(await librarySystem_LabajoContext.ToListAsync());
        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var records = await _context.Records
                .Include(r => r.FK_borrower)
                .Include(r => r.FK_librarian)
                .FirstOrDefaultAsync(m => m.record_id == id);
            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // GET: Records/Create
        public IActionResult Create()
        {

            //========= LIBRARIAN ==========
            // Fetching data from your database context
            var Lib_data = _context.User.Select(x => new {
                CombinedValue = x.FirstName + " " + x.LastName,
                Id = x.id // Assuming you have an Id property or some unique identifier
            }).ToList();

            // Creating a SelectList
            ViewData["librarianName"] = new SelectList(Lib_data, "Id", "CombinedValue");


            //====== BORROWER ============

            var Bor_data = _context.Borrower.Select(x => new {
                CombinedValue = x.b_Id +" : "+ x.b_fname + " " + x.b_lname,
                Id = x.b_Id // Assuming you have an Id property or some unique identifier
            }).ToList();

            // Creating a SelectList
            ViewData["borrowerName"] = new SelectList(Bor_data, "Id", "CombinedValue");




            /*   ViewData["borrowerId"] = new SelectList(_context.Borrower, "b_Id", "b_Course");
               ViewData["librarianId"] = new SelectList(_context.User, "id", "FirstName");*/

            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("record_id,borrowerId,due_date,librarianId,transac_date")] Records records)
        {
            if (ModelState.IsValid)
            {
                _context.Add(records);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["borrowerId"] = new SelectList(_context.Borrower, "b_Id", "b_Course", records.borrowerId);
            ViewData["librarianId"] = new SelectList(_context.User, "id", "FirstName", records.librarianId);
            return View(records);
        }

        // GET: Records/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var records = await _context.Records.FindAsync(id);
            if (records == null)
            {
                return NotFound();
            }
            ViewData["borrowerId"] = new SelectList(_context.Borrower, "b_Id", "b_Course", records.borrowerId);
            ViewData["librarianId"] = new SelectList(_context.User, "id", "FirstName", records.librarianId);
            return View(records);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("record_id,borrowerId,due_date,librarianId,transac_date")] Records records)
        {
            if (id != records.record_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(records);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordsExists(records.record_id))
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
            ViewData["borrowerId"] = new SelectList(_context.Borrower, "b_Id", "b_Course", records.borrowerId);
            ViewData["librarianId"] = new SelectList(_context.User, "id", "FirstName", records.librarianId);
            return View(records);
        }

        // GET: Records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var records = await _context.Records
                .Include(r => r.FK_borrower)
                .Include(r => r.FK_librarian)
                .FirstOrDefaultAsync(m => m.record_id == id);
            if (records == null)
            {
                return NotFound();
            }

            return View(records);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          

            //cheking the data in details
            bool data_Details = _context.Details.Include(d => d.FK_record_id).Any(d => d.record_id == d.FK_record_id.record_id);

          

            if (data_Details == true)
            {
                //if condition is true, this is for the default of the view itself
                var records_Reload = await _context.Records
                   .Include(r => r.FK_borrower)
                   .Include(r => r.FK_librarian)
                   .FirstOrDefaultAsync(m => m.record_id == id);


                ModelState.AddModelError("", "The account is existing in details.");

                return View(records_Reload);
            }
            else
            {
                if (_context.Records == null)
                {
                    return Problem("Entity set 'LibrarySystem_LabajoContext.Records'  is null.");
                }
                var records = await _context.Records.FindAsync(id);
                if (records != null)
                {
                    _context.Records.Remove(records);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


              
        }

        private bool RecordsExists(int id)
        {
          return (_context.Records?.Any(e => e.record_id == id)).GetValueOrDefault();
        }
    }
}
