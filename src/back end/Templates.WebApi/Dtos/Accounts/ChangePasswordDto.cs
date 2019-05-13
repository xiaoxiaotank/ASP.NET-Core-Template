using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Templates.WebApi.Core.Resources;

namespace Templates.WebApi.Dtos.Accounts
{
    public class ChangePasswordDto
    {
        public int Id { get; set; }

        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }

        [Display(Name = "新密码")]
        public string NewPassword { get; set; }
    }

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.OldPassword).NotEmpty().WithMessage(DtoValidation.NotEmpty);
            RuleFor(m => m.NewPassword).NotEmpty().WithMessage(DtoValidation.NotEmpty).Length(6, 20).WithMessage(DtoValidation.Length);
        }
    }

}
