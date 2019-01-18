using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Templates.WebAPI.Attributes.Filters
{
    /// <summary>
    /// 模型验证过滤器
    /// 2.1 later 版本框架自动注册了全局模型验证
    /// </summary>
    public class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errorMessage = string.Join(
                Environment.NewLine, 
                context.ModelState.Select(kvp => $"{kvp.Key}: {kvp.Value.Errors.First().ErrorMessage}"));
            context.Result = new BadRequestObjectResult(errorMessage);
        }
    }
}
