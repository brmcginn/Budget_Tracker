using Budget_Tracker.Models;
using Budget_Tracker;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budget_Tracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<BudgetCategories> BudgetCategories { get; set; }
        public DbSet<Budgets> Budgets { get; set; }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
    }
}
