﻿using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebApi.Core.Resources;

namespace Templates.WebApi.Dtos.Users
{
    /// <summary>
    /// 用户创建
    /// </summary>
    public class UserPostDto
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }
    }

    public class UserPostValidator : AbstractValidator<UserPostDto>
    {
        public UserPostValidator()
        {
            RuleFor(m => m.UserName).NotNull().WithMessage(DtoValidation.NotNull).Length(2, 8).WithMessage(DtoValidation.Length);
            RuleFor(m => m.Name).NotNull().WithMessage(DtoValidation.NotNull).Length(2, 4).WithMessage(DtoValidation.Length);
        }
    }
}
