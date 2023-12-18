using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Linq;

namespace Epicurious.MVC.Controllers
{
    public class LikeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IToastNotification _toastNotification;

        public LikeController(UnitOfWork unitOfWork, UserManager<User> userManager, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        // Action to star a recipe
        [HttpPost]
        public async Task<IActionResult> StarRecipe(Guid recipeId)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(recipeId);

            var currentUser = await _userManager.GetUserAsync(User);

            if (recipe != null && currentUser != null)
            {
                // TODO : currentUser.LikedRecipes is null. Establish a control mechanism for whether the user likes the recipe or not?
                //var likedRecipe = currentUser.LikedRecipes.FirstOrDefault(lr => lr.RecipeId == recipeId, null);
                //if (likedRecipe != null)
                //{
                //    _toastNotification.AddErrorToastMessage("You are Already Like This Recipe");
                //    return RedirectToAction($"RecipePage/{recipeId}", "Recipe"); // Redirect to the recipe listing or details page
                //}

                var recipeStar = new LikedRecipe
                {
                    Id = Guid.NewGuid(),
                    Recipe = recipe,
                    RecipeId = recipe.Id,
                    User = currentUser,
                    UserId = currentUser.Id,
                    CreatedByUserId = currentUser.Id,
                };  
                _toastNotification.AddSuccessToastMessage("You Liked This Recipe");
                _unitOfWork.LikedRecipeRepository.Add(recipeStar);
            }

            return RedirectToAction("Index", "Recipe"); // Redirect to the recipe listing or details page
        }

        // Action to unstar a recipe
        public async Task<IActionResult> UnstarRecipe(Guid recipeId)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var recipeStar = _unitOfWork.LikedRecipeRepository.GetAll().
                SingleOrDefault(rs => rs.RecipeId == recipeId && rs.UserId == currentUser.Id);

            if (recipeStar != null)
            {
                _unitOfWork.LikedRecipeRepository.Delete(recipeStar);
            }

            return RedirectToAction("Index"); // Redirect to the recipe listing or details page
        }
    }
}
