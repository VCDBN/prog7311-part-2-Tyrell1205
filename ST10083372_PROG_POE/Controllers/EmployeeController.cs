using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10083372_PROG_POE.Models;
using System.Data;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FarmersDB.Models;

namespace ST10083372_PROG_POE.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly FarmersDbContext _context;

        public EmployeeController(FarmersDbContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                // Validate the user credentials
                var (isValidUser, role) = await ValidateUser(login.Email, login.Password);

                if (isValidUser)
                {
                    // Authenticate the user
                    await Authenticate(login.Email, role);

                    if (role == "Farmer")
                    {
                        // Redirect to the AddProduct
                        return RedirectToAction("AddProduct", "Product");
                    }
                    else if (role == "Employee")
                    {
                        // Redirect to Add Farmer, the create page
                        return RedirectToAction("Create", "Farmer");
                    }
                }

                ModelState.AddModelError("", "Invalid Email or Password");
            }

            return View(login);
        }

        // Validate the user credentials
        //Following code was developed with the help of Microsoft, QueryableExtensions. You can find it at:
        //https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.queryableextensions.firstordefaultasync?view=entity-framework-6.2.0
        private async Task<(bool, string)> ValidateUser(string email, string password)
        {
            // Check if the user is an Employee
            Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
            if (employee != null)
            {
                return (true, "Employee");
            }
            // Check if the user is a Farmer
            Farmer farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Email == email && f.Password == password);
            if (farmer != null)
            {
                return (true, "Farmer");
            }

            return (false, null);
        }

        // Authenticate the user
        //Following code was developed with the help of Microsoft, Authorization. You can find it at:
        //https://learn.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-7.0
        private async Task Authenticate(string email, string userRole)
        {
            var claims = new[] {
        new Claim(ClaimTypes.NameIdentifier, email),
        new Claim(ClaimTypes.Role, userRole)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}