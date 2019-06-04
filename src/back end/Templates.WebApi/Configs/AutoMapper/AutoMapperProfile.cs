using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Dtos.Accounts;
using Templates.WebApi.Dtos.Users;

namespace Templates.WebApi.Configs
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserLoginedDto>();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
