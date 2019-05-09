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
        private static readonly IReadOnlyDictionary<Type, (int StatusCode, string Value)> _mappingDic = new Dictionary<Type, (int StatusCode, string Value)>()
        {
            [typeof(UnauthorizedAccessException)] = (StatusCodes.Status401Unauthorized, "Unauthorized request"),
            [typeof(NotFoundException)] = (StatusCodes.Status404NotFound, "Not found"),
        };
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
            var result = new ObjectResult("An error has occurred.");

            if (exception is AppException)
            {
                var ex = exception as AppException;
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Value = ex.Message;
            }
            else if (_mappingDic.TryGetValue(exception.GetType(), out var tuple))
            {
                result.StatusCode = tuple.StatusCode;
                result.Value = tuple.Value;
            }
            else
            {
                _logger.LogError(exception, exception.Message);

                if (_env.IsDevelopment())
                {
                    throw exception;
                }

                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = "An unhandled server error has occurred.";
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
