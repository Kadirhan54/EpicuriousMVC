using Epicurious.Domain.Identity;

namespace Epicurious.Domain.Entities
{
    public class Recipe : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string ImageUrl { get; set; }

        public bool? IsApproved { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<LikedRecipe> Likes { get; set; }
    }
}
