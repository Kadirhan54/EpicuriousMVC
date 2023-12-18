using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class Comment : EntityBase<Guid>
    {
        public Guid CommentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
