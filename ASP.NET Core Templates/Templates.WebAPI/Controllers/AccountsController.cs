using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templates.Application.Users;
using Templates.WebAPI.Dtos.Accounts;

namespace Templates.WebAPI.Controllers
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
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<AccountDto> Login([FromBody]LoginDto dto)
        {
            return new AccountDto();
        }

        [HttpDelete]
        public void Logout()
        {

        }


        [HttpPut]
        public void ChangePassword([FromBody]ChangePasswordDto dto)
        {

        }
    }
}