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
    public class MigrationsDbContextFactory: IDesignTimeDbContextFactory<ConfigDbContext>
    {
        public ConfigDbContext CreateDbContext(string[] args)
        {
            //use default connection string
            var builder = new DbContextOptionsBuilder<ConfigDbContext>();
            var connectionString = "Server=(local);Initial catalog=VehicleConfigurator; Integrated Security=True;";
            builder.UseSqlServer(connectionString);

            return new ConfigDbContext(builder.Options);
        }
    }
}
