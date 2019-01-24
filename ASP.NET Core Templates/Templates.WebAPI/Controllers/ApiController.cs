using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Templates.WebAPI.Entities;

namespace Templates.WebAPI.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    //[Authorize]
    public class ApiController : ControllerBase
    {
        public CurrentUser CurrentUser => new CurrentUser
        {
            Id = int.Parse(User.FindFirst(JwtClaimTypes.Id).Value),
            UserName = User.FindFirst(JwtClaimTypes.Name).Value
        };


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TQueryDto"></typeparam>
        /// <param name="query"></param>
        /// <param name="isComplete">指示查询表达式是否已完成</param>
        /// <returns></returns>
        [NonAction]
        public Expression<Func<T, bool>> GetQueryExpression<T, TQueryDto>(TQueryDto query, out bool isComplete) where TQueryDto : class
        {
            isComplete = query == null;
            return t => true;
        }
    }
}