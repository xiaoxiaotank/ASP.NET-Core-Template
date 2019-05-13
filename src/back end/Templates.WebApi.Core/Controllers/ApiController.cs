using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Templates.WebApi.Core.Dtos;
using Templates.WebApi.Core.Models;

namespace Templates.WebApi.Core.Controllers
{
    [Route("api")]
    [Produces("application/json")]
#warning 正式版发布时，需要启用授权验证
    //[Authorize]
    public class ApiController : ControllerBase
    {
        public CurrentUserModel CurrentUser => new CurrentUserModel
        {
            Id = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value),
            UserName = User.FindFirst(JwtClaimTypes.Name).Value
        };

        [HttpDelete("deleteFiles")]
        public ActionResult DeleteFiles([FromBody]IEnumerable<string> urls, [FromServices]ILogger<ApiController> logger)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TQueryDto"></typeparam>
        /// <param name="queryDto"></param>
        /// <param name="queryExp"></param>
        /// <returns></returns>
        [NonAction]
        protected bool IsQueryExpressionComplete<T, TQueryDto>(TQueryDto queryDto, ref Expression<Func<T, bool>> queryExp) where TQueryDto : QueryDto
        {
            if (queryExp == null)
            {
                queryExp = t => true;
            }

            return queryDto == null;
        }
    }
}
