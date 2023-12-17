

using Epicurious.Domain.Enums;

using System.ComponentModel.DataAnnotations;

namespace Epicurious.MVC.ViewModels
{
    public class AuthRegisterViewModel
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
    }
}
