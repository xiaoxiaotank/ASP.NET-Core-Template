using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Models;

namespace Templates.WebAPI.Dtos.Accounts
{
    public class AccountDto
    {
        public UserLoginedDto User { get; set; }

        public JwtResponse Jwt { get; set; }
    }


    public class UserLoginedDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
