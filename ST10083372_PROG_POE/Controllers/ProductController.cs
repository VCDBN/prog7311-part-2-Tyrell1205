using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10083372_PROG_POE.Models;

namespace ST10083372_PROG_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly FarmersDbContext _dbContext;

        public ProductController(FarmersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Farmer/AddProduct
        [Authorize(Policy = "FarmerPolicy")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        
        [Authorize(Policy = "FarmerPolicy")]
        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the Product entity
                var product = new Product
                {
                    ProductName = model.ProductName,
                    SupplyDate = model.SupplyDate,
                    ProductType = model.ProductType
                };

                // Add the product to the database
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                // Redirect to the farmer's dashboard or a success page
                return RedirectToAction("AddProduct", "Product");
            }

            // If the model is not valid, return the view with validation errors
            return View(model);


        }

        [Authorize(Policy = "EmployeePolicy")]
        [HttpGet]
        public IActionResult Filter()
        {
            return View();
        }
        [Authorize(Policy = "EmployeePolicy")]
        [HttpPost]
        public IActionResult Filter(string productName, DateTime? startDate, DateTime? endDate, string productType)
        {
            // Create a query to retrieve products from the database
            var query = _dbContext.Products.AsQueryable();

            // Apply filters based on the provided parameters
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.SupplyDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.SupplyDate <= endDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(productType))
            {
                var lowercaseProductType = productType.ToLower();
                query = query.Where(p => p.ProductType.ToLower().Equals(lowercaseProductType));
            }
            // Retrieve the filtered products as a list
            var filteredProducts = query.ToList();
            // Return the view with the filtered products
            return View(filteredProducts);


        }



    }
}
    

