using Microsoft.AspNetCore.Mvc;

namespace OnlineFood.Controllers
{
    public class AdminController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
