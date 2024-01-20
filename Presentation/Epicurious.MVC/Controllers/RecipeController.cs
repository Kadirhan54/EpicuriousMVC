using Epicurious.Application.Dtos.Recipe;
using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.MVC.Validators;
using Epicurious.MVC.ViewModels;
using Epicurious.Persistence.UnitofWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<User> _userManager;

        public RecipeController(UnitOfWork unitOfWork, IToastNotification toastNotification, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.RecipeRepository.Include(x=>x.Likes,x=>x.User));
        }

        // Recipe  görüntüleme
        [HttpGet]
        public IActionResult RecipePage(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id,x=>x.Likes,x=>x.Comments,x=>x.User);

            if (recipe == null)
            {
                return NotFound("Reposityory not found!ASDASDA");
            }

            return View(recipe);
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            var addRecipeViewModel = new RecipeViewModel();
            return View(addRecipeViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddRecipeAsync(AddRecipeDto addRecipeDto)
        {
            if (!ModelState.IsValid)
            {
                var validator = new AddRecipeDtoValidator();
                var validationResult = validator.Validate(addRecipeDto);

                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }

                return View(addRecipeDto);
            }
            // Accessing the user's ID
            //var userId = _userManager.GetUserId(User);

            //// Getting the entire user object
            var user = await _userManager.GetUserAsync(User);

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = user.Id,
                Title = addRecipeDto.Title,
                Ingredients = addRecipeDto.Ingredients,
                Description = addRecipeDto.Description,
                ImageUrl = addRecipeDto.ImageUrl,
                User = user,
                UserId = user.Id,
                Category = new Category()
                {
                    Name="Deneme Category"
                }
            };

            _unitOfWork.RecipeRepository.Add(recipe);

            _toastNotification.AddSuccessToastMessage("Successfully created Recipe!");

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult UpdateRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound("ASHJDGASHJDS");
            }

            var updateRecipeViewModel = new UpdateRecipeDto
            {
                Title = recipe.Title,
                Ingredients = recipe.Ingredients,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
            };


            return View(updateRecipeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRecipeAsync(Guid id,UpdateRecipeDto updateRecipeDto)
        {
            if (ModelState.IsValid)
            {
                var existingRecipe = _unitOfWork.RecipeRepository.GetById(id);

                if (existingRecipe == null)
                {
                    return NotFound("QWHJKEKJQW");
                }

                existingRecipe.Title = updateRecipeDto.Title;
                existingRecipe.Ingredients = updateRecipeDto.Ingredients;
                existingRecipe.Description = updateRecipeDto.Description;
                existingRecipe.ImageUrl = updateRecipeDto.ImageUrl;

                _unitOfWork.RecipeRepository.Update(existingRecipe);
                _toastNotification.AddSuccessToastMessage("Recipe updated succeed!");

                return RedirectToAction(nameof(Index));
            }

            return View(updateRecipeDto);
        }
        //Delete
        [HttpGet]
        public IActionResult DeleteRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        public IActionResult ConfirmDeleteRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _unitOfWork.RecipeRepository.Delete(recipe);
            _toastNotification.AddSuccessToastMessage("Recipe deleted succeed!");

            return RedirectToAction(nameof(Index));
        }

    }
}


