using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

using System;

namespace HotelListing.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.File(path: @"C:\Users\joaop\OneDrive\Ambiente de Trabalho\HotelListingLogs\log-.txt",
                                       outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                       rollingInterval: RollingInterval.Day,
                                       restrictedToMinimumLevel: LogEventLevel.Information
                                      )
                        .CreateLogger();

            try
            {
                Log.Information("Application is starting!");
                CreateHostBuilder(args).Build()
                                       .Run();
            }
            catch (Exception)
            {
                Log.Fatal("Something went wrong!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
