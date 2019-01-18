using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Models;

namespace Templates.WebAPI.Dtos.Users
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

        private UserDto(User entity)
        {
            Id = entity.Id;
            UserName = entity.UserName;
            Name = entity.Name;
            Email = entity.Email;
        }

        public static implicit operator UserDto(User entity) => new UserDto(entity);

    }
}
