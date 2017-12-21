namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.Infrastructure;
    using DAL.Context;

    /// <summary>
    /// Default migrations configuration
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Context.ConfigDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            TargetDatabase = new DbConnectionInfo(ConfigDbContext.DefaultConnectionName);
        }

        protected override void Seed(DAL.Context.ConfigDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
