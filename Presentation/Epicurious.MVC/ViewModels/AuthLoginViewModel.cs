
using System;
using System.ComponentModel.DataAnnotations;



namespace Epicurious.MVC.ViewModels
{
    public class AuthLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
