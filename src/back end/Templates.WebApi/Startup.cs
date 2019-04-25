using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Templates.Application.Users;
using Templates.Common.Extensions;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Entities;
using Templates.WebApi.Core.Attributes.Filters;

[assembly: ApiController]
namespace Templates.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }


        /// <summary>
        /// 该方法由运行时调用，用来向容器添加服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<TemplateDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));

            #region 服务依赖注入
            services.AddTransient<IUserAppService, UserAppService>();
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DbContext, TemplateDbContext>();
            #endregion

            services.AddJwtBearerAuthentication(Configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Templates.WebAPI", Version = "v1" });

                var commentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var commentFilePath = Path.Combine(AppContext.BaseDirectory, commentFileName);
                options.IncludeXmlComments(commentFilePath);
            });

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
                //options.SuppressMapClientErrors = true;
                //禁用： ModelState.IsValid 400 响应类型 ValidationProblemDetails
                //options.SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;

                //如果发生404响应，则转到相应连接
                //options.ClientErrorMapping[404].Link = "https://webapi感觉根本不需要.com/404";
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
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Templates.WebAPI v1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            loggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration("Configs/nlog.config");

            app.UseCors("angular");
            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyOrigin();
            //});
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
