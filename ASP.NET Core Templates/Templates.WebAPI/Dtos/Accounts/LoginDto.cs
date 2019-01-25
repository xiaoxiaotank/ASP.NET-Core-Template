using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Accounts
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; }

        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(m => m.UserNameOrEmail).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
        }
    }
}
