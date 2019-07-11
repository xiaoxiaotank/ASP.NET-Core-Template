using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Templates.Application.Authentications;
using Templates.Application.Users;
using Templates.Common.Extensions;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Configs;
using Templates.WebApi.Core.Attributes.Filters;

//会将 Web API 行为应用到程序集中的所有控制器,无法针对单个控制器执行选择退出操作。
[assembly: ApiController]
namespace Templates.WebApi
{
    public class Startup
    {
        //private Timer _timer = new Timer((state) =>
        //{
        //    throw new Exception();
        //}, null, 10 * 1000, 1000 * 1000);

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostEnvironment { get; }


        /// <summary>
        /// 该方法由运行时调用，用来向容器添加服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<TemplateDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));

            #region 服务依赖注入
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IAuthenticationAppService, AuthenticationAppService>();
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DbContext, TemplateDbContext>();
            #endregion

            services.AddMvc(options => 
            {
                options.Filters.Add<ExceptionHandlerFilterAttribute>();
                options.Filters.Add<ModelStateValidationFilterAttribute>();

            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .ConfigureApiBehaviorOptions(options =>
            {
                //禁用： [FromForm]默认解析为 multipart/form-data 请求内容类型
                //options.SuppressConsumesConstraintForFormFileParameters = true;
                //禁用：[FromXXX]默认推理规则
                //options.SuppressInferBindingSourcesForParameters = true;
                //禁用： ModelState.IsValid 默认行为（触发400响应）
                //options.SuppressModelStateInvalidFilter = true;
                //禁用：4XX 响应类型 ProblemDetails
                options.SuppressMapClientErrors = true;
                //禁用： ModelState.IsValid 400 响应类型 ValidationProblemDetails
                //options.SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;

                //替换404响应 ProbleDetails 的Type属性值
                //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://tools.ietf.org/html/rfc7231#section-6.5.4";

                //（使用fluent api无视）当 SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true 时，可以自定义错误响应
                //options.InvalidModelStateResponseFactory = ctx =>
                //{
                //    var problemDetails = new ValidationProblemDetails(ctx.ModelState)
                //    {
                //        //提供一个参考文档路径
                //        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                //        Title = "出现了一个或多个模型验证错误",
                //        Status = StatusCodes.Status400BadRequest,
                //        Detail = "请查看 errors 属性了解详细错误信息",
                //        Instance = ctx.HttpContext.Request.Path
                //    };
                //
                //    return new BadRequestObjectResult(problemDetails)
                //    {
                //        ContentTypes = { "application/proble+json" }
                //    };
                //};
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .AddFluentValidation(config => 
            {
                //注册 Startup 所属程序集下所有的 public nonabstract Validators
                config.RegisterValidatorsFromAssemblyContaining<Startup>();
                //禁用 DataAnnotations 和 IValidatableObject
                config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                //验证复杂对象时不再(不应，否则执行两次)需要使用 SetValidator 
                config.ImplicitlyValidateChildProperties = true;
            });

            services.AddJwtBearerAuthentication(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("angular", p =>
                {
                    p.WithOrigins("http://localhost:10000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            //8.x版本直接使用Mapper.Initialize会报错
            services.AddAutoMapper(typeof(Startup));

#if DEBUG
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Templates.WebAPI", Version = "v1" });

                var commentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentFilePath = Path.Combine(AppContext.BaseDirectory, commentFileName);
                options.IncludeXmlComments(commentFilePath);
            });
#endif
        }

        /// <summary>
        /// 该方法由运行时调用，用来配置HTTP请求管道（中间件）
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
#if DEBUG
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Templates.WebAPI v1");
                });
#endif
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseCors("angular");
            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyOrigin();
            //});
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            //不能通过 UseMvc 定义的传统路由或通过 Startup.Configure 中的 UseMvcWithDefaultRoute 访问Web API操作。
        }

    }
}
