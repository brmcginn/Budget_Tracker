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
    public class UserBudgets
    {
        [Required]
        public Guid ID { get; set; }

        [ForeignKey("Budgets")]
        public Budgets Budget { get; set; }

        [ForeignKey("ApplicationUsers")]
        public ApplicationUsers User { get; set; }

    }
}
