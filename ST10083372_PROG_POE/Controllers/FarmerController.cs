using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10083372_PROG_POE.Models;
using System.Data;
using System.Net;

namespace ST10083372_PROG_POE.Controllers
{
    public class FarmerController : Controller
    {
        private readonly FarmersDbContext _context;

        public FarmerController(FarmersDbContext context)
        {
            _context = context;
        }
        //Authorize code was developed with the help off C# corner, Forms Authentication in MVC. You can find it at:
        //https://www.c-sharpcorner.com/article/forms-authentication-in-mvc/
        
        // GET: Farmer/Create

        [Authorize(Policy = "EmployeePolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Farmer/Create
       [Authorize(Policy = "EmployeePolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Farmer");//redirect to the add farmer page
            }

            return View(farmer);
        }
    }
}

