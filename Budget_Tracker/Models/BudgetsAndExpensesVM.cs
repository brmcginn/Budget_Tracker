using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget_Tracker.Models
{
    public class BudgetsAndExpensesVM
    {
        public List<ApplicationUsers> applicationUsers { get; set; }
        public List<BudgetCategories> budgetCategories { get; set; }
        public List<Budgets> budgets { get; set; }
        public List<Expenses> expenses { get; set; }
    }
}
