using System;
using System.ComponentModel.DataAnnotations;

namespace Epicurious.MVC.ViewModels
{
    public class RecipeViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Description { get; set; }
    }
}


