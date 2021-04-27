using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Budget_Tracker.Models
{
    public class ApplicationUsers : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
