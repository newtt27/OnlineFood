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
            var categories = await _context.FoodCategories.ToListAsync();

            var categoryData = categories
                .Select((fc, index) => new
                {
                    label = fc.TenDanhMuc,
                    value = _context.Foods.Count(f => f.IdDanhMuc == fc.Id),
                    color = _colors[index % _colors.Count]
                })
                .ToList();

            return Json(categoryData);
        }

        private readonly List<string> _colors = new List<string>
        {
            "#FF5733",
            "#33FF57",
            "#3357FF",
            "#FF33A8",
            "#FFC300",
            "#8D33FF",
            "#33FFF5"
        };
    }
}