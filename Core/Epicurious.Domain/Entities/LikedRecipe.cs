using Epicurious.Domain.Identity;

namespace Epicurious.Domain.Entities
{
    public class LikedRecipe : EntityBase<Guid>
    {

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
