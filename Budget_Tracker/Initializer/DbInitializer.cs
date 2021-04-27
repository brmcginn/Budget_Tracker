using Budget_Tracker.Data;
using Budget_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Budget_Tracker
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }catch(Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == SD.Admin)) return;

            System.Diagnostics.Debug.WriteLine("Please actually work");

            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.User)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUsers
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name="MIS 421"

            }, "Admin123*").GetAwaiter().GetResult();

            ApplicationUsers user = _db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUsers
            {
                UserName = "smdaly@crimson.ua.edu",
                Email = "smdaly@crimson.ua.edu",
                EmailConfirmed = true,
                Name = "Sean"

            }, "BasicPassword123*").GetAwaiter().GetResult();

            ApplicationUsers user1 = _db.ApplicationUsers.Where(u => u.Email == "smdaly@crimson.ua.edu").FirstOrDefault();
            _userManager.AddToRoleAsync(user1, SD.User).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUsers
            {
                UserName = "aprodgers2@crimson.ua.edu",
                Email = "aprodgers2@crimson.ua.edu",
                EmailConfirmed = true,
                Name = "Alec"

            }, "BasicPassword123*").GetAwaiter().GetResult();

            ApplicationUsers user2 = _db.ApplicationUsers.Where(u => u.Email == "aprodgers2@crimson.ua.edu").FirstOrDefault();
            _userManager.AddToRoleAsync(user2, SD.User).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUsers
            {
                UserName = "brmcginn@crimson.ua.edu",
                Email = "brmcginn@crimson.ua.edu",
                EmailConfirmed = true,
                Name = "Ben"

            }, "BasicPassword123*").GetAwaiter().GetResult();

            ApplicationUsers user3 = _db.ApplicationUsers.Where(u => u.Email == "brmcginn@crimson.ua.edu").FirstOrDefault();
            _userManager.AddToRoleAsync(user3, SD.User).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUsers
            {
                UserName = "gspurrier@cba.ua.edu",
                Email = "gspurrier@cba.ua.edu",
                EmailConfirmed = true,
                Name = "Gary"

            }, "BasicPassword123*").GetAwaiter().GetResult();

            ApplicationUsers user4 = _db.ApplicationUsers.Where(u => u.Email == "gspurrier@cba.ua.edu").FirstOrDefault();
            _userManager.AddToRoleAsync(user4, SD.User).GetAwaiter().GetResult();


        }
    }
}
