namespace Epicurious.MVC.ViewModels
{
    public class AddCommentModel
    {
        public Guid RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}