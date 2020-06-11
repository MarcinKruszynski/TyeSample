using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Data;
using ProductService.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);

            host.MigrateDbContext<ProductContext>((context, services) =>
                {                    
                    var logger = services.GetService<ILogger<ProductContextSeed>>();

                    new ProductContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                });
            
            host.Run();            
        }

        private static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                             
                .UseStartup<Startup>()                
                .Build();        
    }
}
