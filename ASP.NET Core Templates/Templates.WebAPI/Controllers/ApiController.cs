using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Templates.WebAPI.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    //[Authorize]
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="isComplete">指示查询表达式是否已完成</param>
        /// <returns></returns>
        [NonAction]
        public Expression<Func<T, bool>> GetQueryExpression<T>(object query, out bool isComplete)
        {
            isComplete = query == null;
            return t => true;
        }
    }
}