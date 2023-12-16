using Epicurious.Domain.Entities;
using Epicurious.MVC.ViewModels;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epicurious.MVC.Controllers
{
    [Authorize] // Bu controller'a sadece yetkili kullanıcıların erişebilmesi için
    public class RecipeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public RecipeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Recipe listesini görüntüleme
        public IActionResult Index()
        {
            return View(_unitOfWork.RecipeRepository.GetAll());
        }

        // Recipe  görüntüleme
        [HttpGet("/recipe/{id:Guid}")]
        public IActionResult RecipePage(Guid id)
        {
            // Use the 'id' parameter to retrieve the specific recipe based on the provided GUID
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                // Handle the case where the recipe with the specified ID is not found
                return NotFound("Reposityory not found!");
            }

            // You might want to pass the retrieved recipe to the view
            return View(recipe);
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
                    Comment = new Comment { CreatedByUserId = User.Identity.Name },
                    CreatedByUserId = User.Identity.Name,
                };

                _unitOfWork.RecipeRepository.Add(recipe);

                return RedirectToAction(nameof(Index));
            }

            return View(addRecipeViewModel);
        }
    }
}


