using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Templates.WebApi.Core.Dtos;
using Templates.WebApi.Core.Entities;

namespace Templates.WebApi.Core.Controllers
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
