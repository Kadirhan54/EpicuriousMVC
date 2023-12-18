using Epicurious.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class LikedRecipe : EntityBase<Guid>
    {
        // Foreign key to Recipe
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        // Foreign key to User
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
