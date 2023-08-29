using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Models;
using Hotel.Models.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HotelContext>();
                context.Officials.Add(new Official() { Id = Guid.NewGuid(), Name = "Sefa", Surname = "�zt�rk", Title = "M�d�r"});
                context.Officials.Add(new Official() { Id = Guid.NewGuid(), Name = "Cengiz", Surname = "Veli", Title = "M�d�r" });
                context.Officials.Add(new Official() { Id = Guid.NewGuid(), Name = "O�uz", Surname = "Kurt", Title = "M�d�r" });
                context.SaveChanges();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
