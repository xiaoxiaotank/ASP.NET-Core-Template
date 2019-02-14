using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.WebApi.Dtos.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? DeletionTime { get; set; }

        private UserDto(User entity)
        {
            Id = entity.Id;
            UserName = entity.UserName;
            Name = entity.Name;
            Email = entity.Email;
            IsDeleted = entity.IsDeleted;
            CreationTime = entity.CreationTime;
            DeletionTime = entity.DeletionTime;
        }

        public static implicit operator UserDto(User entity) => new UserDto(entity);

    }
}
