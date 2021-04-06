using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget_Tracker.Models
{
    public class BudgetCategories
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name too long")]
        public string Name { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [StringLength(100, ErrorMessage = "Description too long")]
        public string Description { get; set; }

        [Required]
        public Guid BudgetID { get; set; }

        [ForeignKey("BudgetID")]
        public Budgets Budget { get; set; }

    }
}
