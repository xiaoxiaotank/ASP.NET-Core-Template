using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Templates.WebApi.Core.Attributes.Filters
{
    /// <summary>
    /// 异常处理过滤器（捕获会话所有异常）
    /// </summary>
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionHandlerFilterAttribute> _logger;
        private readonly IHostingEnvironment _env;

        public ExceptionHandlerFilterAttribute(ILogger<ExceptionHandlerFilterAttribute> logger, IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            IActionResult result = null;

            switch (exception)
            {
                case AppException ex:
                    result = new BadRequestObjectResult(ex.Message);
                    break;
                case UnauthorizedAccessException ex:
                    result = new UnauthorizedObjectResult(ex.Message);
                    break;
                case NotFoundException ex:
                    result = new NotFoundObjectResult(ex.Message);
                    break;
                default:
                    _logger.LogError(exception, exception.Message);

                    if (_env.IsDevelopment())
                    {
                        throw exception;
                    }

                    result = new ObjectResult("An unhandled server error has occurred.")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    break;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
