using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_Tracker;
using Budget_Tracker.Data;
using Budget_Tracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Budget_Tracker.Views
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = SD.User + "," + SD.Admin)]
        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expenses.Include(e => e.BudgetCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = SD.User + "," + SD.Admin)]
        public async Task<IActionResult> ExpensesCategoriesAndBudgets()
        {
            BudgetsAndExpensesVM vm = new BudgetsAndExpensesVM();
            vm.expenses = _context.Expenses.ToList();
            vm.budgetCategories = _context.BudgetCategories.ToList();
            vm.budgets = _context.Budgets.ToList();
            return View(vm);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .Include(e => e.BudgetCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["BudgetCategoryID"] = new SelectList(_context.BudgetCategories, "ID", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,User,Name,Date,Description,Amount,Recurring,BudgetCategoryID,BudgetCategory")] Expenses expenses)
        {
            if (ModelState.IsValid)
            {
                expenses.ID = Guid.NewGuid();
                expenses.User = HttpContext.User.Identity.Name;
                _context.Add(expenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetCategoryID"] = new SelectList(_context.BudgetCategories, "ID", "Name", expenses.BudgetCategoryID);
            return View(expenses);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }
            ViewData["BudgetCategoryID"] = new SelectList(_context.BudgetCategories, "ID", "Name", expenses.BudgetCategoryID);
            return View(expenses);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,User,Name,Date,Description,Amount,Recurring,BudgetCategoryID,BudgetCategory")] Expenses expenses)
        {
            if (id != expenses.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpensesExists(expenses.ID))
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
            ViewData["BudgetCategoryID"] = new SelectList(_context.BudgetCategories, "ID", "Name", expenses.BudgetCategoryID);
            return View(expenses);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses
                .Include(e => e.BudgetCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var expenses = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensesExists(Guid id)
        {
            return _context.Expenses.Any(e => e.ID == id);
        }
    }
}
