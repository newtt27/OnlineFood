using Microsoft.AspNetCore.Mvc;

namespace OnlineFood.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
