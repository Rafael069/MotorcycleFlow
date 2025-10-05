using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Core.Entities
{
    // Base class that will be used by different classes, but will not be instantiated
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString(); 
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
