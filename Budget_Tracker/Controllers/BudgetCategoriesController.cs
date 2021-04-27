using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_Tracker.Data;
using Budget_Tracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Budget_Tracker.Views
{
    public class BudgetCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = SD.User + "," + SD.Admin)]
        // GET: BudgetCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BudgetCategories.Include(b => b.Budget);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BudgetCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategories = await _context.BudgetCategories
                .Include(b => b.Budget)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budgetCategories == null)
            {
                return NotFound();
            }

            return View(budgetCategories);
        }

        // GET: BudgetCategories/Create
        public IActionResult Create()
        {
            ViewData["BudgetID"] = new SelectList(_context.Budgets, "ID", "Name");
            return View();
        }

        // POST: BudgetCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Amount,Description,BudgetID")] BudgetCategories budgetCategories)
        {
            if (ModelState.IsValid)
            {
                budgetCategories.ID = Guid.NewGuid();
                _context.Add(budgetCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BudgetID"] = new SelectList(_context.Budgets, "ID", "Name", budgetCategories.BudgetID);
            return View(budgetCategories);
        }

        // GET: BudgetCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategories = await _context.BudgetCategories.FindAsync(id);
            if (budgetCategories == null)
            {
                return NotFound();
            }
            ViewData["BudgetID"] = new SelectList(_context.Budgets, "ID", "Name", budgetCategories.BudgetID);
            return View(budgetCategories);
        }

        // POST: BudgetCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Amount,Description,BudgetID")] BudgetCategories budgetCategories)
        {
            if (id != budgetCategories.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetCategoriesExists(budgetCategories.ID))
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
            ViewData["BudgetID"] = new SelectList(_context.Budgets, "ID", "Name", budgetCategories.BudgetID);
            return View(budgetCategories);
        }

        // GET: BudgetCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCategories = await _context.BudgetCategories
                .Include(b => b.Budget)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budgetCategories == null)
            {
                return NotFound();
            }

            return View(budgetCategories);
        }

        // POST: BudgetCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var budgetCategories = await _context.BudgetCategories.FindAsync(id);
            _context.BudgetCategories.Remove(budgetCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetCategoriesExists(Guid id)
        {
            return _context.BudgetCategories.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Report(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            BudgetsAndExpensesVM vm = new BudgetsAndExpensesVM();
            vm.expenses = _context.Expenses.ToList();
            vm.budgetCategories = _context.BudgetCategories.ToList();
            vm.budgets = _context.Budgets.ToList();

            BudgetsAndExpensesVM actualList = new BudgetsAndExpensesVM();

            List<BudgetCategories> actualBC = new List<BudgetCategories>();

            foreach (var item in vm.budgetCategories)
            {
                if (item.ID == id)
                {
                    actualBC.Add(item);
                }
            }

            List<Expenses> actualExpense = new List<Expenses>();


            foreach (var item in vm.expenses)
            {
                if (item.BudgetCategoryID == id)
                {
                    actualExpense.Add(item);
                }
            }

            
            actualList.budgetCategories = actualBC;
            actualList.expenses = actualExpense;


            if (actualList == null)
            {
                return NotFound();
            }

            return View(actualList);
        }
    }
}

