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
    }
}


