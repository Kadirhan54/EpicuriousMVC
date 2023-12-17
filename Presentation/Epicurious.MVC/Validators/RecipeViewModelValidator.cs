using Epicurious.MVC.ViewModels;
using FluentValidation;

namespace Epicurious.MVC.Validators
{
    public class RecipeViewModelValidator : AbstractValidator<RecipeViewModel>
    {
        public RecipeViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(10).WithMessage("Title is have to be longer than 10");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description is have to be longer than 10");
            RuleFor(x => x.Ingredients).NotEmpty().WithMessage("Ingredients is required").MinimumLength(10).WithMessage("Ingredients is have to be longer than 10");
        }
    }
}
