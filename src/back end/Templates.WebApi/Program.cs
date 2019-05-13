using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Templates.WebApi
{
    public class Program
    {
        private static readonly Logger _logger;

        static Program()
        {
            _logger = NLogBuilder.ConfigureNLog(@"Configs\nlog.config").GetCurrentClassLogger();
        }

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "由于发生致命性异常，程序终止运行");
                throw;
            }
            finally
            {
                //程序终止运行时，为了避免NLog出现不必要的异常，手动关闭
                //如果日志记录为异步，则会出现日志记录丢失
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .UseNLog();


        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            _logger.Fatal(exception, exception.Message);
        }
    }
}
