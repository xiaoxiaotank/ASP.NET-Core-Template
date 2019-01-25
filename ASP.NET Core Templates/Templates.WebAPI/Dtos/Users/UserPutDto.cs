using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebAPI.Resources;

namespace Templates.WebAPI.Dtos.Users
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
            RuleFor(m => m.Id).NotEmpty().WithMessage(Validation.NotEmpty);
            RuleFor(m => m.Name).NotNull().WithMessage(Validation.NotNull).Length(2, 4).WithMessage(Validation.Length);
            RuleFor(m => m.Email).EmailAddress().When(m => m.Email != null).WithMessage(Validation.EmailAddress);
        }
    }
}
