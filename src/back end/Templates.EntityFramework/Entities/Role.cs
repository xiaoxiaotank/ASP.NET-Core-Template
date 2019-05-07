using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.EntityFrameworkCore.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public DateTime? DeletionTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
