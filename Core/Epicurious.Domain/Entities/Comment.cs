using Epicurious.Domain.Identity;

namespace Epicurious.Domain.Entities
{
    public class Comment : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
