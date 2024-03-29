﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templates.Application.Authentications;
using Templates.Application.Users;
using Templates.WebApi.Core.Controllers;
using Templates.WebApi.Core.Models;
using Templates.WebApi.Dtos.Accounts;

namespace Templates.WebApi.Controllers
{
    /// <summary>
    ///  同步版本演示
    /// </summary>
    [Obsolete]
    public class AccountsSyncController : ApiController
    {
        private readonly IAuthenticationAppService _authAppService;

        public AccountsSyncController(IAuthenticationAppService authAppService)
        {
            _authAppService = authAppService;
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
            var user = _authAppService.Login(dto.UserNameOrEmail, dto.Password);
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
                Jwt = new TokenModel(user.Id.ToString(), user.UserName).ToJwtResponse(configuration)
            };
        }

        [HttpDelete("logout")]
        public void Logout()
        {

        }


        [HttpPut("changepwd")]
        public IActionResult ChangePassword([FromBody]ChangePasswordDto dto)
        {
            _authAppService.ChangePassword(dto.Id, dto.OldPassword, dto.NewPassword);

            return NoContent();
        }
    }
}