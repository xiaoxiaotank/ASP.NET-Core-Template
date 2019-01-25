using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Users
{
    /// <summary>
    /// 用户创建
    /// </summary>
    public class UserPostDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }
    }

    public class UserPostValidator : AbstractValidator<UserPostDto>
    {
        public UserPostValidator()
        {
            RuleFor(m => m.UserName).NotNull().Length(2, 8);
            RuleFor(m => m.Name).NotNull().Length(2, 4);
        }
    }
}
