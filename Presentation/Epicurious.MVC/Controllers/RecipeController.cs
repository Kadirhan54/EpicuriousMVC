using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Epicurious.MVC.ViewModels;
using System.Collections.Generic;
using Epicurious.Persistence.UnitOfWork;
using Epicurious.Domain.Entities;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        //private readonly List<RecipeViewModel> _recipes = new List<RecipeViewModel>();

        public RecipeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Recipe listesini görüntüleme
        public IActionResult Index()
        {
            return View(_unitOfWork.RecipeRepository.GetAll());
        }

        // Recipe eklemek için get action
        [HttpGet]
        public IActionResult AddRecipe()
        {
            var addRecipeViewModel = new RecipeViewModel(); // Recipe eklemek için bir ViewModel olduğunu varsayalım
            return View(addRecipeViewModel);
        }

        // Recipe eklemek için post action
        [HttpPost]
        public IActionResult AddRecipeAsync(RecipeViewModel addRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                // Yeni bir Recipe nesnesi oluşturdum ve listeye ekledim
                var recipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = addRecipeViewModel.Title,
                    Ingredients = addRecipeViewModel.Ingredients,
                    Description = addRecipeViewModel.Description,
                    Comment = new Comment { }
                };

                _unitOfWork.RecipeRepository.Add(recipe);
                //_recipes.Add(recipe);

                return RedirectToAction(nameof(Index));
            }

            return View(addRecipeViewModel);
        }
    }
}


