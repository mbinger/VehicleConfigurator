using DAL.Common.Equipment;
using DAL.Common.Reference;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL.Seed
{
    public class ConfigDbContextSeeder
    {
        private enum ColorType
        {
            Normal = 1,
            Mat = 2,
            Metalic = 3
        }

        public enum RimType
        {
            Steel = 1,
            Alu = 2,
            Forged = 3
        }

        public enum FuelType
        {
            Gas = 1,
            Diesel = 2
        }

        public void EnsureSeedData(ConfigDbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                var anyData = context.ColorTypes.Any() ||
                    context.OrderStatuses.Any() ||
                    context.RimTypes.Any() ||
                    context.FuelTypes.Any() ||
                    context.Cars.Any() ||
                    context.Colors.Any() ||
                    context.Engines.Any() ||
                    context.Rims.Any() ||
                    context.AdditionalEquipmentItems.Any();

                if (!anyData)
                {
                    ImportJson(context, context.ColorTypes, "ColorTypes");
                    ImportJson(context, context.FuelTypes, "FuelTypes");
                    ImportJson(context, context.OrderStatuses, "OrderStatuses");
                    ImportJson(context, context.RimTypes, "RimTypes");
                    ImportJson(context, context.Colors, "Colors");
                    ImportJson(context, context.Engines, "Engines");
                    ImportJson(context, context.Rims, "Rims");
                    ImportJson(context, context.Cars, "Cars");
                    ImportJson(context, context.AdditionalEquipmentItems, "AdditionalEquipmentItems");

                    transaction.Commit();
                }
            }
        }

        private void ImportJson<T>(DbContext context, DbSet<T> dbSet, string tableName) where T : class
        {
            using (var rdr = new StreamReader($"Seed\\{tableName}.json", Encoding.UTF8))
            {
                var jsonString = rdr.ReadToEnd();
                var deserializedCollection = JsonConvert.DeserializeObject<List<T>>(jsonString);
                foreach (var item in deserializedCollection)
                {
                    dbSet.Add(item);
                }
            }

            try
            {
                var cmd = $"SET IDENTITY_INSERT {tableName} ON";
                context.Database.ExecuteSqlCommand(cmd);
                context.SaveChanges();
            }
            finally
            {
                var cmd = $"SET IDENTITY_INSERT {tableName} OFF";
                context.Database.ExecuteSqlCommand(cmd);
            }

        }

    }
}