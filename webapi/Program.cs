using Domain.Models.Tables;
using Domain.Repositories.EFInitial;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webapi;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHost(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                //await context.Database.MigrateAsync();
                //await Seed.SeedData(context, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseUrls("http://*:7177");
                    webBuilder.UseStartup<Startup>();
                });
    }
}