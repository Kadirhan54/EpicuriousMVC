using Microsoft.AspNetCore.Mvc;

namespace Epicurious.MVC.ViewModels
{
    public class CommentViewModel 
    {
        public string CreatedByUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
