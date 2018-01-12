using DAL.Context;
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
    }
}
