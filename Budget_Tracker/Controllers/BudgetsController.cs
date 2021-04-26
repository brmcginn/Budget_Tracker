using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_Tracker.Data;
using Budget_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Budget_Tracker.Views
{
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = SD.User + "," + SD.Admin)]
        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Budgets.ToListAsync());
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgets = await _context.Budgets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budgets == null)
            {
                return NotFound();
            }

            return View(budgets);
        }

        // GET: Budgets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Amount,Description,StartDate,EndDate")] Budgets budgets)
        {
            if (ModelState.IsValid)
            {
                budgets.ID = Guid.NewGuid();
                budgets.User = HttpContext.User.Identity.Name;
                _context.Add(budgets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(budgets);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgets = await _context.Budgets.FindAsync(id);
            if (budgets == null)
            {
                return NotFound();
            }
            return View(budgets);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Amount,Description,StartDate,EndDate")] Budgets budgets)
        {
            if (id != budgets.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetsExists(budgets.ID))
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
            return View(budgets);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgets = await _context.Budgets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budgets == null)
            {
                return NotFound();
            }

            return View(budgets);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var budgets = await _context.Budgets.FindAsync(id);
            _context.Budgets.Remove(budgets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetsExists(Guid id)
        {
            return _context.Budgets.Any(e => e.ID == id);
        }
    }
}
