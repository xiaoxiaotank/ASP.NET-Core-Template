using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templates.EntityFrameworkCore.Models;
using Templates.WebAPI.Dtos.Users;

namespace Templates.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {
            
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            var results = new User[]
            {
                new User
                {
                    Id = 1,
                    UserName = "xiao",
                    Name = "jjj",
                    Email = "jjj@test.com"
                },
                new User
                {
                    Id = 2,
                    UserName = "feng",
                    Name = "lll",
                    Email = "lll@test.com"
                }
            }.Select(u => (UserDto)u);
            return results;
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">返回指定id的用户</response>
        /// <resonse code="404">指定id的用户不存在</resonse>
        [HttpGet("{id}")]
        //用于Swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> Get(int id)
        {
            if(id < 1)
            {
                return NotFound();
            }

            UserDto result = new User
            {
                Id = id,
                UserName = "TestUserName",
                Name = "TestName",
                Email = "TestEmail"
            };
            return result;
        }

        [HttpPost]
        public ActionResult<UserDto> Post([FromBody]UserCreateDto dto)
        {
            //return CreatedAtAction(nameof(Get), new { dto })
            return Ok();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
