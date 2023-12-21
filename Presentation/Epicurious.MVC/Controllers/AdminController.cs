using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace Epicurious.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public AdminController(UnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.RecipeRepository.Include(x => x.Likes, x => x.User));
        }

        // GET: /admin/reviewpage
        [HttpGet]
        public IActionResult ReviewRecipe(Guid id) 
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);
            return View(recipe);
        }

        // POST: /admin/approvereceipe/{recipeId}
        [HttpPost]
        public IActionResult ApproveRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong while approving recipe!");
                return RedirectToAction("ReviewRecipe", "Admin", new { id });
            }

            recipe.IsApproved = true;
            _unitOfWork.RecipeRepository.Update(recipe);
            _toastNotification.AddSuccessToastMessage("Successfully Approved Recipe!");

            return RedirectToAction("ReviewRecipe", "Admin", new{ id });

        }

        // POST: /admin/unapprovereceipe/{recipeId}
        [HttpPost]
        public IActionResult UnapproveRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong while unapproving recipe!");
                return RedirectToAction("ReviewRecipe", "Admin", new { id });
            }

            recipe.IsApproved = false;
            _unitOfWork.RecipeRepository.Update(recipe);
            _toastNotification.AddSuccessToastMessage("Successfully Approved Recipe!");

            return RedirectToAction("ReviewRecipe", "Admin", new { id });
        }
    }
}
