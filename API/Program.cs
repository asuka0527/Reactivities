using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        // change to async method
        public static async Task Main(string[] args)
        {
            // Configured to automatically do migrations if we dont have any database setup
          var host=  CreateHostBuilder(args).Build();

          using var scope = host.Services.CreateScope();

          var services = scope.ServiceProvider;

          try
          {
              var context = services.GetRequiredService<DataContext>();

              var userManager = services.GetRequiredService<UserManager<AppUser>>();
             
              await context.Database.MigrateAsync();
              await Seed.SeedData(context, userManager);
          }
          catch (System.Exception ex)
          {
              var logger = services.GetRequiredService<ILogger<Program>>();
              logger.LogError(ex, "An error occured during migration");
              
          }

        // to start application
          await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
