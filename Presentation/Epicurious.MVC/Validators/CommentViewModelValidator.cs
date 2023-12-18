using Epicurious.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Epicurious.MVC.Validators
{
    public class CommentViewModelValidator : AbstractValidator<CommentViewModel>
    {
        public CommentViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(10).WithMessage("Title is have to be longer than 10");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MinimumLength(10).WithMessage("Description is have to be longer than 10");
        }
    }
}
