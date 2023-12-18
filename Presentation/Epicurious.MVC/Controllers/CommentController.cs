using Microsoft.AspNetCore.Mvc;

namespace Epicurious.MVC.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
