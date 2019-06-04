using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Templates.Application.Authentications;
using Templates.WebApi.Core.Controllers;
using Templates.WebApi.Core.Models;
using Templates.WebApi.Dtos.Accounts;

namespace Templates.WebApi.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IAuthenticationAppService _authAppService;
        private readonly IMapper _mapper;

        public AccountsController(
            IAuthenticationAppService authAppService,
            IMapper mapper)
        {
            _authAppService = authAppService;
            _mapper = mapper;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AccountDto>> LoginAsync([FromBody]LoginDto dto, [FromServices]IConfiguration configuration)
        {
            var user = await _authAppService.LoginAsync(dto.UserNameOrEmail, dto.Password);

            return new AccountDto()
            {
                User = _mapper.Map<UserLoginedDto>(user),
                Jwt = new TokenModel(user.Id.ToString(), user.UserName).ToJwtResponse(configuration)
            };
        }

        [HttpDelete("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Logout()
        {

        }


        [HttpPut("changepwd")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto dto)
        {
            await _authAppService.ChangePasswordAsync(dto.Id, dto.OldPassword, dto.NewPassword);

            return NoContent();
        }
    }
}