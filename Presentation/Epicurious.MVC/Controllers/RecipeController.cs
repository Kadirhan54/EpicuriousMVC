using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Epicurious.MVC.ViewModels;
using System.Collections.Generic;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly List<RecipeViewModel> _recipes = new List<RecipeViewModel>();

        // Recipe listesini görüntüleme
        public IActionResult Index()
        {
            return View(_recipes);
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
        public IActionResult AddRecipe(RecipeViewModel addRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                // Yeni bir Recipe nesnesi oluşturdum ve listeye ekledim
                var recipe = new RecipeViewModel
                {
                    Title = addRecipeViewModel.Title,
                    Ingredients = addRecipeViewModel.Ingredients,
                    Description = addRecipeViewModel.Description,
                    // Diğer özellikler işte
                };

                _recipes.Add(recipe);

                return RedirectToAction(nameof(Index));
            }

            return View(addRecipeViewModel);
        }
    }
}


