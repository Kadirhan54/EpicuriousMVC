using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Epicurious.Persistence.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NToastNotify;
using System.Threading;

namespace Epicurious.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private const string CacheKey = "RecipeKey";

        public AdminController(UnitOfWork unitOfWork, IToastNotification toastNotification, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
            _memoryCache = memoryCache;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30),
                Priority = CacheItemPriority.High,
            };
        }

        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(CacheKey, out var recipes)) { return View(recipes); }

            recipes = await _unitOfWork.RecipeRepository.Include(x => x.Likes, x => x.User).AsNoTracking().ToListAsync(cancellationToken);

            _memoryCache.Set(CacheKey, recipes, _cacheEntryOptions);

            return View(recipes);
        }

        // GET: /admin/reviewpage
        [HttpGet]
        public IActionResult ReviewRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);
            return View(recipe);
        }

        // POST: /admin/approvereceipe/{recipeId}
        [HttpPost]
        public IActionResult ApproveRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong while approving recipe!");
                return RedirectToAction("ReviewRecipe", "Admin", new { id });
            }

            recipe.IsApproved = true;
            _unitOfWork.RecipeRepository.Update(recipe);
            _toastNotification.AddSuccessToastMessage("Successfully Approved Recipe!");

            return RedirectToAction("ReviewRecipe", "Admin", new { id });

        }

        // POST: /admin/unapprovereceipe/{recipeId}
        [HttpPost]
        public IActionResult UnapproveRecipe(Guid id)
        {
            var recipe = _unitOfWork.RecipeRepository.GetById(id);

            if (recipe == null)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong while unapproving recipe!");
                return RedirectToAction("ReviewRecipe", "Admin", new { id });
            }

            recipe.IsApproved = false;
            _unitOfWork.RecipeRepository.Update(recipe);
            _toastNotification.AddSuccessToastMessage("Successfully Approved Recipe!");

            return RedirectToAction("ReviewRecipe", "Admin", new { id });
        }
    }
}
