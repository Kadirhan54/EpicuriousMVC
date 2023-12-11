using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class SavedRecipes
    {
        public Guid SavedRecipeId { get; set; }

        // Foreign key to Recipe
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        // Other properties for a saved recipe
    }
}
