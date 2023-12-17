using Epicurious.MVC.ViewModels;
using FluentValidation;

namespace Epicurious.MVC.Validators
{
    public class AuthRegisterViewModelValidator : AbstractValidator<AuthRegisterViewModel>
    {
        public AuthRegisterViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").MinimumLength(10).WithMessage("Email is have to be longer than 10");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required").MinimumLength(4).WithMessage("Username is have to be longer than 4");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(10).WithMessage("Password is have to be longer than 10");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required").MinimumLength(3).WithMessage("Firstname is have to be longer than 3");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("SurName is required").MinimumLength(3).WithMessage("Surname is have to be longer than 3");
            //RuleFor(x => x.BirthDate).GreaterThanOrEqualTo();
        }
    }

}
