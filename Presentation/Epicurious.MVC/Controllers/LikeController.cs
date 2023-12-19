using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            /////////////////////////////////// VERSION 1 ///////////////////////////////////////

            //var recipe = _unitOfWork.RecipeRepository.GetById(recipeId);

            //var currentUser = await _userManager.Users.
            //    Include(u => u.LikedRecipes).
            //    FirstOrDefaultAsync(u => u.Id == Guid.Parse(_userManager.GetUserId(User)));

            //if (recipe != null && currentUser != null)
            //{
            //    var alreadyLiked = currentUser.LikedRecipes.Any(lr => lr.RecipeId == recipeId);
            //    if (alreadyLiked)
            //    {
            //        _toastNotification.AddErrorToastMessage("You have already liked this recipe");
            //        return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });
            //    }

            //    var recipeStar = new LikedRecipe
            //    {
            //        Id = Guid.NewGuid(),
            //        Recipe = recipe,
            //        RecipeId = recipe.Id,
            //        User = currentUser,
            //        UserId = currentUser.Id,
            //        CreatedByUserId = currentUser.Id,
            //    };  
            //    _toastNotification.AddSuccessToastMessage("You Liked This Recipe");
            //    _unitOfWork.LikedRecipeRepository.Add(recipeStar);
            //}

            //return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });

            /////////////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////// VERSION 2 ///////////////////////////////////////

            var recipe = _unitOfWork.RecipeRepository.GetById(recipeId);

            var user = await _userManager.GetUserAsync(User);

            var isUserLikedRecipe = _unitOfWork.LikedRecipeRepository.GetAll().Where(x => x.UserId == user.Id).Where(x => x.RecipeId == recipeId).FirstOrDefault();

            if (recipe != null && user != null)
            {
                if (isUserLikedRecipe !=null)
                {
                    _toastNotification.AddErrorToastMessage("You have already liked this recipe");
                    return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });
                }

                var recipeStar = new LikedRecipe
                {
                    Id = Guid.NewGuid(),
                    Recipe = recipe,
                    RecipeId = recipe.Id,
                    User = user,
                    UserId = user.Id,
                    CreatedByUserId = user.Id,
                };
                _toastNotification.AddSuccessToastMessage("You Liked This Recipe");
                _unitOfWork.LikedRecipeRepository.Add(recipeStar);
            }

            return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });

            /////////////////////////////////////////////////////////////////////////////////////
        }

        // Action to unstar a recipe
        public async Task<IActionResult> UnstarRecipe(Guid recipeId)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(recipeId);

            var user = await _userManager.GetUserAsync(User);

            var isUserLikedRecipe = _unitOfWork.LikedRecipeRepository.GetAll().Where(x => x.UserId == user.Id).Where(x => x.RecipeId == recipeId).FirstOrDefault();

            if (recipe != null && user != null)
            {
                if (isUserLikedRecipe == null)
                {
                    _toastNotification.AddErrorToastMessage("You have already unliked this recipe");
                    return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });
                }

                _unitOfWork.LikedRecipeRepository.Delete(isUserLikedRecipe);
                _toastNotification.AddSuccessToastMessage("You Unliked This Recipe");
            }

            return RedirectToAction("RecipePage", "Recipe", new { id = recipeId });
        }
    }
}
