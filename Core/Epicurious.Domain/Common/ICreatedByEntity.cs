using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Domain.Common
{
    public interface ICreatedByEntity
    {
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
