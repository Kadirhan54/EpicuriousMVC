using Epicurious.Application.Dtos.Recipe;
using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.MVC.Validators;
using Epicurious.MVC.ViewModels;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Epicurious.MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<User> _userManager;

        public CommentController(UnitOfWork unitOfWork, IToastNotification toastNotification, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return View();
        }
        //if(!ModelState.IsValid)
        //{
        //var validator = new AddRecipeDtoValidator();
        //var validationResult = validator.Validate(addRecipeDto);

        //foreach (var error in validationResult.Errors)
        //{
        //    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //    _toastNotification.AddErrorToastMessage(error.ErrorMessage);
        //}

        //return View(addRecipeDto);
        //}

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(AddCommentModel addCommentModel,[FromRoute] Guid id)
        {


            var recipe = _unitOfWork.RecipeRepository.GetById(id);
            var user = await _userManager.GetUserAsync(User);

            if (recipe == null)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong while Adding comment to recipe!");
                return RedirectToAction("Index", "Recipe");
            }

            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                Title = addCommentModel.Title,
                Description = addCommentModel.Description,
                RecipeId = recipe.Id,
                Recipe = recipe,
                User = user,
                UserId = user.Id
            };

            _unitOfWork.CommentRepository.Add(comment);
            _toastNotification.AddSuccessToastMessage("Successfully Added Comment!");

            return RedirectToAction("RecipePage", "Recipe", new { id = recipe.Id });
        }
    }
}
