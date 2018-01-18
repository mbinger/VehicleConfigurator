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
        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public ConfigDbContext CreateDbContext(string[] args)
        {
            //use default connection string
            var builder = new DbContextOptionsBuilder<ConfigDbContext>();
            var connectionString = _configuration.GetConnectionString(Def.DefaultConnectionName);
            builder.UseSqlServer(connectionString);

            return new ConfigDbContext(builder.Options);
        }
    }
}
