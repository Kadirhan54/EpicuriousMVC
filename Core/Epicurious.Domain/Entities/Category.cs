using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Entities
{
    public class Category : EntityBase<Guid>
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
