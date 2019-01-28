using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebApi.Core.Resources;

namespace Templates.WebApi.Dtos.Accounts
{
    public class LoginDto
    {
        [Display(Name = "用户名或邮箱")]
        public string UserNameOrEmail { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(m => m.UserNameOrEmail).NotEmpty().WithMessage(Validation.NotEmpty);
            RuleFor(m => m.Password).NotEmpty().WithMessage(Validation.NotEmpty);
        }
    }
}
