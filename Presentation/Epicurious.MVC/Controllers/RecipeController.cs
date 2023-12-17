using Epicurious.Domain.Entities;
using Epicurious.MVC.ViewModels;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly List<RecipeViewModel> _recipes = new List<RecipeViewModel>();
        private readonly IToastNotification _toastNotification;


        public RecipeController(UnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
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
            var addRecipeViewModel = new RecipeViewModel();
            return View(addRecipeViewModel);
        }

        // Recipe eklemek için post action
        [HttpPost]
        public IActionResult AddRecipe(RecipeViewModel addRecipeViewModel)
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
                    Comment = new Comment { CreatedByUserId = User.Identity.Name },
                    CreatedByUserId = User.Identity.Name,
                };

                _unitOfWork.RecipeRepository.Add(recipe);
                _toastNotification.AddSuccessToastMessage("Recipe added succeed!");

                return RedirectToAction(nameof(Index));
            }

            return View(addRecipeViewModel);
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
                Id = recipe.Id,
                Title = recipe.Title,
                Ingredients = recipe.Ingredients,
                Description = recipe.Description,
                Comment = new Comment { CreatedByUserId = User.Identity.Name },
                CreatedByUserId = User.Identity.Name,
            };


            return View(updateRecipeViewModel);
        }
        [HttpPost]
        public IActionResult UpdateRecipe(RecipeViewModel updateRecipeViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingRecipe = _unitOfWork.RecipeRepository.GetById(updateRecipeViewModel.Id);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                existingRecipe.Title = updateRecipeViewModel.Title;
                existingRecipe.Ingredients = updateRecipeViewModel.Ingredients;
                existingRecipe.Description = updateRecipeViewModel.Description;

                _unitOfWork.RecipeRepository.Update(existingRecipe);
                _toastNotification.AddSuccessToastMessage("Recipe updated succeed!");

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


