﻿using Epicurious.Application.Dtos.Recipe;
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
            return View(_unitOfWork.RecipeRepository.GetAll());
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
            var userId = _userManager.GetUserId(User);

            //// Getting the entire user object
            //var user = await _userManager.GetUserAsync(User);

            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                CreatedByUserId = Guid.Parse(userId),
                Title = addRecipeDto.Title,
                Ingredients = addRecipeDto.Ingredients,
                Description = addRecipeDto.Description,
                ImageUrl = addRecipeDto.ImageUrl,
            };

            _unitOfWork.RecipeRepository.Add(recipe);

            _toastNotification.AddSuccessToastMessage("Successfully created Recipe!");

            return RedirectToAction(nameof(Index));

        }

        //Update
        [HttpGet]
        public IActionResult UpdateRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var updateRecipeViewModel = new RecipeViewModel
            {
                //Id = recipe.Id,
                Title = recipe.Title,
                Ingredients = recipe.Ingredients,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                //Comment = new Comment { CreatedByUserId = User.Identity.Name },
                CreatedByUserId = User.Identity.Name,
            };


            return View(updateRecipeViewModel);
        }
        [HttpPost]
        public IActionResult UpdateRecipe(RecipeViewModel updateRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                //var existingRecipe = _unitOfWork.RecipeRepository.GetById(updateRecipeViewModel.Id);

                //if (existingRecipe == null)
                //{
                //    return NotFound();
                //}

                //existingRecipe.Title = updateRecipeViewModel.Title;
                //existingRecipe.Ingredients = updateRecipeViewModel.Ingredients;
                //existingRecipe.Description = updateRecipeViewModel.Description;

                //_unitOfWork.RecipeRepository.Update(existingRecipe);
                //_toastNotification.AddSuccessToastMessage("Recipe updated succeed!");

                return RedirectToAction(nameof(Index));
            }

            return View(updateRecipeViewModel);
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


