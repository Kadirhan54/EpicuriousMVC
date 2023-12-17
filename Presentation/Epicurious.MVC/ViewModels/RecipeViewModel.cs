using System;
using System.ComponentModel.DataAnnotations;
using Epicurious.Domain.Entities;

namespace Epicurious.MVC.ViewModels
{
    public class RecipeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Description { get; set; }

        public Comment Comment { get; set; }
        public string CreatedByUserId { get; set; }

    }
}


