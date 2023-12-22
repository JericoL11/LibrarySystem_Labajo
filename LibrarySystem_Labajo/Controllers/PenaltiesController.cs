﻿using System;
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
    public class PenaltiesController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public PenaltiesController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        // GET: Penalties
        public async Task<IActionResult> Index()
        {
              return _context.Penalty != null ? 
                          View(await _context.Penalty.ToListAsync()) :
                          Problem("Entity set 'LibrarySystem_LabajoContext.Penalty'  is null.");
        }

        // GET: Penalties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Penalty == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalty
                .FirstOrDefaultAsync(m => m.Penalty_Id == id);
            if (penalty == null)
            {
                return NotFound();
            }

            return View(penalty);
        }

        // GET: Penalties/Create
        public IActionResult Create()
        {
            ViewData["details_id"] = new SelectList(_context.Details, "details_id", "details_id");

            return View();
        }

        // POST: Penalties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Penalty_Id,Amount,Penalty_date,IsSettled,P_details_Id,Penalty_name")] Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penalty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["details_id"] = new SelectList(_context.Details, "details_id", "details_id");
            return View(penalty);
        }

        // GET: Penalties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Penalty == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalty.FindAsync(id);
            if (penalty == null)
            {
                return NotFound();
            }
            return View(penalty);
        }

        // POST: Penalties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Penalty_Id,Amount,Penalty_date,IsSettled,P_details_Id,Penalty_name")] Penalty penalty)
        {
            if (id != penalty.Penalty_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penalty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltyExists(penalty.Penalty_Id))
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
            return View(penalty);
        }

        // GET: Penalties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Penalty == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalty
                .FirstOrDefaultAsync(m => m.Penalty_Id == id);
            if (penalty == null)
            {
                return NotFound();
            }

            return View(penalty);
        }

        // POST: Penalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Penalty == null)
            {
                return Problem("Entity set 'LibrarySystem_LabajoContext.Penalty'  is null.");
            }
            var penalty = await _context.Penalty.FindAsync(id);
            if (penalty != null)
            {
                _context.Penalty.Remove(penalty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltyExists(int? id)
        {
          return (_context.Penalty?.Any(e => e.Penalty_Id == id)).GetValueOrDefault();
        }
    }
}
