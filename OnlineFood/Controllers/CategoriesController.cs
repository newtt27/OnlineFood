using Microsoft.AspNetCore.Mvc;
using OnlineFood.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;

namespace OnlineFood.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly OnlineFoodContext _context;

        public CategoriesController(OnlineFoodContext context)
        {
            _context = context;
        }

        public IActionResult Chart()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData()
        {
            // Sample data to match the visualization
            var chartData = new List<object>
            {
                new { label = "Asian Cuisine", value = 14, color = "#3498db" }, // Blue
                new { label = "Italian Food", value = 16, color = "#e74c3c" },  // Red
                new { label = "Mexican Food", value = 12, color = "#2ecc71" },  // Green
                new { label = "Fast Food", value = 8, color = "#f1c40f" },      // Yellow
                new { label = "Others", value = 2, color = "#95a5a6" }          // Grey
            };

            return Json(chartData);
        }
    }
}