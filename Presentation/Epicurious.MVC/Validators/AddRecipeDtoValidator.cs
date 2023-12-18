using Epicurious.Application.Dtos.Recipe;
using FluentValidation;

namespace Epicurious.MVC.Validators
{
    public class AddRecipeDtoValidator : AbstractValidator<AddRecipeDto>
    {
        public AddRecipeDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(10).WithMessage("Title is have to be longer than 10");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description is have to be longer than 10");
            RuleFor(x => x.Ingredients).NotEmpty().WithMessage("Ingredients is required").MinimumLength(10).WithMessage("Ingredients is have to be longer than 10");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image is required").MinimumLength(10).WithMessage("Image is have to be longer than 10");
        }
    }
}
