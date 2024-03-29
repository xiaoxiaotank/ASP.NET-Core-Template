﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebApi.Core.Resources;

namespace Templates.WebApi.Dtos.Users
{
    public class UserPutDto
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        public bool? Gender { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }

    public class UserPutValidator : AbstractValidator<UserPutDto>
    {
        public UserPutValidator()
        {
            RuleFor(m => m.Id).NotEmpty().WithMessage(DtoValidation.NotEmpty);
            RuleFor(m => m.Name).NotNull().WithMessage(DtoValidation.NotNull).Length(2, 4).WithMessage(DtoValidation.Length);
            RuleFor(m => m.Email).EmailAddress().When(m => m.Email != null).WithMessage(DtoValidation.EmailAddress);
        }
    }
}
