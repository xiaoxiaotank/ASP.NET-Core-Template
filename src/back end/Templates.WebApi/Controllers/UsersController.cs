﻿using System;
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
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<UsersSyncController> _logger;

        public UsersController(
            IUserAppService userAppService,
            ILogger<UsersSyncController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            _logger.LogInformation("获取所有用户");
            return _userAppService.Get().Select(u => (UserDto)u);
        }

        [HttpGet("by")]
        public IEnumerable<UserDto> Get([FromBody]UserQueryDto query)
        {
            var queryExp = GetQueryExpression(query);
            return _userAppService.Get(queryExp).Select(u => (UserDto)u);
        }

        [HttpGet("page")]
        public IEnumerable<UserDto> Get([FromQuery]int page = 1, [FromQuery]int size = 20)
        {
            return _userAppService.Get()
                .Paged(page, size)
                .Select(u => (UserDto)u);
        }

        [HttpGet("pageby")]
        public IEnumerable<UserDto> Get([FromBody]UserQueryDto query, [FromQuery]int page = 1, [FromQuery]int size = 20)
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
        public async Task<ActionResult<UserDto>> GetAsync([FromRoute]int id)
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
        public async Task<ActionResult<UserDto>> PostAsync([FromBody]UserPostDto dto)
        {
            UserDto result = await _userAppService.CreateAsync(new User
            {
                UserName = dto.UserName,
                Name = dto.Name,
            });

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task PutAsync([FromBody]UserPutDto dto)
        {
            await _userAppService.UpdateAsync(new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Gender = dto.Gender
            });
        }

        [HttpPatch]
        public void Patch([FromBody]UserPatchDto dto)
        {
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute]int id)
        {
            await _userAppService.DeleteAsync(id);
        }

        [HttpDelete]
        public async Task DeleteAsync([FromQuery]IEnumerable<int> ids)
        {
            await _userAppService.DeleteAsync(ids);
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
