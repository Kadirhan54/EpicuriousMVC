using System;
using System.ComponentModel.DataAnnotations;

namespace Epicurious.MVC.ViewModels
{
    public class RecipeViewModel
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
    }
}


