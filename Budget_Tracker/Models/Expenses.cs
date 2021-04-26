using Budget_Tracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget_Tracker
{
    public class Expenses
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name should be less than 30 characters.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Description must be shorter than 100 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Amount must be postitive number.")]
        public double Amount { get; set; }

        [Required]
        public bool Recurring { get; set; }

        [Required]
        public Guid BudgetCategoryID { get; set; }

        [ForeignKey("BudgetCategoryID")]
        public BudgetCategories BudgetCategory { get; set; }

    }
}
