using Epicurious.MVC.ViewModels;
using FluentValidation;

namespace Epicurious.MVC.Validators
{
    public class AuthLoginViewModelValidator  : AbstractValidator<AuthLoginViewModel>
    {
        public AuthLoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").MinimumLength(10).WithMessage("Email is have to longer than 10");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(10).WithMessage("Password is have to longer than 10");
        }
    }
}
