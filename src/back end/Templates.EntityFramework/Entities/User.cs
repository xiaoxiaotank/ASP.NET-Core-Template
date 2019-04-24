using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Templates.EntityFrameworkCore.Entities
{
    public class User : Entity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public DateTime? DeletionTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
