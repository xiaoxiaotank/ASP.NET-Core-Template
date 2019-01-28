using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templates.Application.Users;
using Templates.Common.Models;
using Templates.WebApi.Core.Controllers;
using Templates.WebApi.Dtos.Accounts;

namespace Templates.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public AccountsController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 登录
        /// 使用GET会导致密码显示在URL中
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<AccountDto> Login([FromBody]LoginDto dto, [FromServices]IConfiguration configuration)
        {
            var user = _userAppService.Login(dto.UserNameOrEmail, dto.Password);
            if(user == null)
            {
                throw new AppException("用户名或密码错误！");
            }

            return new AccountDto()
            {
                User = new UserLoginedDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                },
                Jwt = new TokenModel()
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName
                }.ToJwtResponse(configuration)
            };
        }

        [HttpDelete("logout")]
        public void Logout()
        {

        }


        [HttpPut("changepwd")]
        public void ChangePassword([FromBody]ChangePasswordDto dto)
        {

        }
    }
}