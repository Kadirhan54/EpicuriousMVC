using Epicurious.MVC.ViewModels;
using FluentValidation;

namespace Epicurious.MVC.Validators
{
    public class AuthRegisterViewModelValidator : AbstractValidator<AuthRegisterViewModel>
    {
        public AuthRegisterViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("SurName is required");
            //RuleFor(x => x.BirthDate).;
        }
    }
    
}
