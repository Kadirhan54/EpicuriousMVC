using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Identity;

public class LikeController : Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public LikeController(UnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    // Action to star a recipe
    [HttpPost("/like/starrecipe/{recipeId:Guid}")]
    public async Task<IActionResult> StarRecipe(Guid recipeId)
    {
        var recipe = _unitOfWork.RecipeRepository.GetAll().FirstOrDefault(x => x.Id == recipeId);
        var currentUser = await _userManager.GetUserAsync(User);

        if (recipe != null && currentUser != null)
        {
            var recipeStar = new LikedRecipe
            {
                Id = Guid.NewGuid(),
                Recipe = recipe,
                RecipeId = recipe.Id,
                User = currentUser,
                //UserId = currentUser.Id,
                CreatedByUserId = currentUser.UserName,
            };
            _unitOfWork.LikedRecipeRepository.Add(recipeStar);
        }

        return RedirectToAction("Index","Recipe"); // Redirect to the recipe listing or details page
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
