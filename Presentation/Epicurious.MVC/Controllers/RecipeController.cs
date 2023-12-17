using Epicurious.Domain.Entities;
using Epicurious.MVC.Validators;
using Epicurious.MVC.ViewModels;
using Epicurious.Persistence.UnitofWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public RecipeController(UnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.RecipeRepository.GetAll());
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            var addRecipeViewModel = new RecipeViewModel();
            return View(addRecipeViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddRecipeAsync(RecipeViewModel addRecipeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var validator = new RecipeViewModelValidator();
                var validationResult = validator.Validate(addRecipeViewModel);

                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }

                return View(addRecipeViewModel);
            }

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = addRecipeViewModel.Title,
                Ingredients = addRecipeViewModel.Ingredients,
                Description = addRecipeViewModel.Description,
                Comment = new Comment { CreatedByUserId = User.Identity.Name },
                CreatedByUserId = User.Identity.Name,
            };

            _unitOfWork.RecipeRepository.Add(recipe);

            _toastNotification.AddSuccessToastMessage("Successfully created Recipe!");

            return RedirectToAction(nameof(Index));

        }
    }
}


