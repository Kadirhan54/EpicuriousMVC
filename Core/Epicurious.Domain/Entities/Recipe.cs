using Epicurious.Domain.Common;
using Epicurious.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class Recipe : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }

        public string ImageUrl { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public bool? IsApproved { get; set; }

        public ICollection<LikedRecipe> Likes { get; set; }

        //public Comment Comment { get; set; }
    }
}
