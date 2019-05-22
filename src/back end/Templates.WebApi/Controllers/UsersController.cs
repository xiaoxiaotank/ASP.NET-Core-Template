using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Templates.Application.Users;
using Templates.Common.Enums;
using Templates.Common.Extensions;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Core.Controllers;
using Templates.WebApi.Dtos.Users;

namespace Templates.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserAppService userAppService,
            ILogger<UsersController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<UserDto> GetAll()
        {
            _logger.LogInformation("获取所有用户");
            return _userAppService.Get().Select(u => (UserDto)u);
        }

        [HttpGet("by")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<UserDto> GetByQuery([FromBody]UserQueryDto query)
        {
            var queryExp = GetQueryExpression(query);
            return _userAppService.Get(queryExp).Select(u => (UserDto)u);
        }

        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<UserDto> GetPaged([FromQuery]int page = 1, [FromQuery]int size = 20)
        {
            return _userAppService.Get()
                .Paged(page, size)
                .Select(u => (UserDto)u);
        }

        [HttpGet("pageby")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<UserDto> GetPagedByQuery([FromBody]UserQueryDto query, [FromQuery]int page = 1, [FromQuery]int size = 20)
        {
            var queryExp = GetQueryExpression(query);
            return _userAppService.Get(queryExp)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(u => (UserDto)u);
        }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <resonse code="404">指定id的用户不存在</resonse>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetByIdAsync([FromRoute]int id)
        {
            var user = await _userAppService.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return (UserDto)user;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDto>> PostAsync([FromBody]UserPostDto dto)
        {
            UserDto result = await _userAppService.CreateAsync(new User
            {
                UserName = dto.UserName,
                Name = dto.Name,
            });

            //返回201并添加Location标头并填充值
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutAsync([FromBody]UserPutDto dto)
        {
            await _userAppService.UpdateAsync(new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Gender = dto.Gender
            });

            return NoContent();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Patch([FromBody]UserPatchDto dto)
        {
            await _userAppService.UpdateAsync(new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Gender = dto.Gender
            });

            return NoContent();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var user = await _userAppService.GetAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            await _userAppService.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromQuery]IEnumerable<int> ids)
        {
            if (ids.IsNotEmpty())
            {
                await _userAppService.DeleteAsync(ids);
            }

            return NoContent();
        }

        #region NoActions

        [NonAction]
        public Expression<Func<User, bool>> GetQueryExpression(UserQueryDto query, Expression<Func<User, bool>> queryExp = null)
        {
            var isComplete = IsQueryExpressionComplete(query, ref queryExp);
            if (isComplete)
            {
                return queryExp;
            }

            var rights = new List<Expression<Func<User, bool>>>();

            if (query.UserName.IsNotNullAndEmpty())
                rights.Add(u => u.UserName.Contains(query.UserName));
            if (query.Name.IsNotNullAndEmpty())
                rights.Add(u => u.Name.Contains(query.Name));
            if (query.StartCreationTime.HasValue)
                rights.Add(u => u.CreationTime >= query.StartCreationTime);
            if (query.StopCreateionTime.HasValue)
                rights.Add(u => u.CreationTime <= query.StopCreateionTime);

            return queryExp.AndAlso(rights);
        }

        #endregion
    }
}
