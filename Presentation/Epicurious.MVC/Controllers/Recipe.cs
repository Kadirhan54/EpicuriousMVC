using Microsoft.AspNetCore.Mvc;

namespace Epicurious.MVC.Controllers
{
    public class Recipe : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
