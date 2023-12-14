using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class Recipe
    {
        public Guid RecipeId {  get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public string Ingredients { get; set; }

        public string ImageUrl { get; set; }

        public Comment Comment { get; set; }
    }
}
