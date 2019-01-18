using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Templates.EntityFrameworkCore.Models;
using Templates.WebAPI.Attributes.Filters;

namespace Templates.WebAPI
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
            services.AddDbContext<MyDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql")));

            services.AddMvc(options => 
            {
                options.Filters.Add(new ExceptionHandlerFilterAttribute(HostEnvironment));
                options.Filters.Add(new ModelStateValidationFilterAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// 该方法由运行时调用，用来配置HTTP请求管道（中间件）
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
