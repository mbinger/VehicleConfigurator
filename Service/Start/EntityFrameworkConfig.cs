using DAL.Context;
using DAL.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Start
{
    /// <summary>
    /// Configure entity framework
    /// </summary>
    public class EntityFrameworkConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<ConfigDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString(DAL.Def.DefaultConnectionName)));
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //execute migration
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigDbContext>();
                context.Database.Migrate();

                //do seeding
                new ConfigDbContextSeeder().EnsureSeedData(context);
            }
        }
    }
}
