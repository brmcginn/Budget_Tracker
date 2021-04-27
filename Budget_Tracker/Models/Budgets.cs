using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Budget_Tracker.Models
{
    public class Budgets
    {
        public Guid ID { get; set; }

        [ForeignKey("ApplicationUsers")]
        public string User { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name must be shorter than 30 characters")]
        public string Name { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Amount must be positive number")]
        public double Amount { get; set; }

        [StringLength(100, ErrorMessage = "Descriptions must be shorter than 100 characters")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
