using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Application.Dtos.Recipe
{
    public class AddRecipeDto
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
