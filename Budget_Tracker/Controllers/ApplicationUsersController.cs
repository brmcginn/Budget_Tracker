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
using Microsoft.AspNetCore.Identity;

namespace Budget_Tracker.Views
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
       

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            {
                return View(await _context.ApplicationUsers.ToListAsync());

 
            }
        }
    }
}
