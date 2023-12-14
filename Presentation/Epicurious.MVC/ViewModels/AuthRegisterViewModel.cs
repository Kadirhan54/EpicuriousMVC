

using Epicurious.Domain.Enums;

using System.ComponentModel.DataAnnotations;

namespace Epicurious.MVC.ViewModels
{
    public class AuthRegisterViewModel
    {
        [Required]
        public string Email { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "UserName must be at least 2 characters.")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SurName { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
    }
}
