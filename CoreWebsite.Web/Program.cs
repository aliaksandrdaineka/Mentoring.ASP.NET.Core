using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CoreWebsite.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                //.UseSerilog((context, config) => 
                //{
                //    config.ReadFrom.Configuration(context.Configuration);
                //})
                //.ConfigureLogging((webHostBuilderContext, loggingBuilder) =>
                //{
                //    loggingBuilder.ClearProviders();
                //    var config = new LoggerConfiguration()
                //    .ReadFrom.Configuration(webHostBuilderContext.Configuration);
                //    loggingBuilder.AddSerilog(config.CreateLogger());
                //})
                .UseStartup<Startup>();
        }
    }
}
