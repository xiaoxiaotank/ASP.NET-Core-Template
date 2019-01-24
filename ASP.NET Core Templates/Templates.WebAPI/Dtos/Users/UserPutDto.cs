using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Dtos.Users
{
    public class UserPutDto
    {
        [Required]
        public int? Id { get; set; }

        [Required, MinLength(2), MaxLength(8)]
        public string Name { get; set; }

        public bool? Gender { get; set; }
    }
}
