using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget_Tracker.Models
{
    public class Budgets
    {
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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
