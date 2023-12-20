using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Epicurious.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ApproveRecipe(int recipeId)
        {
            //var recipe = _context.Recipes.Find(recipeId);

            //if (recipe != null)
            //{
            //    recipe.IsApproved = true;
            //    _context.SaveChanges();
            //}

            return RedirectToAction("Index"); // Redirect to the admin dashboard or recipe list
        }
    }
}
