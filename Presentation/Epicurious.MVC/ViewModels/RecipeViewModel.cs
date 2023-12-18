using System;
using System.ComponentModel.DataAnnotations;
using Epicurious.Domain.Entities;

namespace Epicurious.MVC.ViewModels
{
    public class RecipeViewModel
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Comment Comment { get; set; }
        public string CreatedByUserId { get; set; }

    }
}


