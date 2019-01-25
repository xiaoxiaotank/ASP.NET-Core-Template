using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Users
{
    public class UserPutDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool? Gender { get; set; }
    }

    public class UserPutValidator : AbstractValidator<UserPutDto>
    {
        public UserPutValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
            RuleFor(m => m.Name).NotNull().Length(2, 4);
        }
    }
}
