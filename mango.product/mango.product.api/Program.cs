using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordering.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateAndSeedDatabase(host);
            //CreateHostBuilder(args).Build()
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void CreateAndSeedDatabase(IHost host)
        {
 
            //using (var scope = host.Services.CreateScope())
            /*{
                var loggerFactory = (ILoggerFactory)host.Services.GetService(typeof(ILoggerFactory));

                try
                {
                    OrderContext aspnetRunContext = (OrderContext)host.Services.GetService(typeof(OrderContext));
                    OrderContextSeed.SeedAsync(aspnetRunContext, loggerFactory).Wait();
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error occurred seeding the DB.");
                }
            }*/
        }


    }
    
}
