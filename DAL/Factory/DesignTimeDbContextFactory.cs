using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.Factory
{
    /// <summary>
    /// Create and configure db context for design time
    /// </summary>
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<ConfigDbContext>
    {
        public ConfigDbContext CreateDbContext(string[] args)
        {
            //load configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Def.AppSettingsFileName)
                .Build();

            //use default connection string
            var builder = new DbContextOptionsBuilder<ConfigDbContext>();
            var connectionString = configuration.GetConnectionString(Def.DefaultConnectionName);
            builder.UseSqlServer(connectionString);

            return new ConfigDbContext(builder.Options);
        }
    }
}
