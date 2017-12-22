using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Reference;
using DAL.Context;

namespace DAL.Migrations
{
    //drop-and-recreate debug migratinos configuration
    public class ConfigurationDropAndCreateDatabaseIfModelChanges: DropCreateDatabaseIfModelChanges<ConfigDbContext>
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

        protected override void Seed(ConfigDbContext context)
        {
            base.Seed(context);

            #region References

            context.ColorTypes.Add(new ColorTypeRef
            {
                Id = (long)ColorType.Normal,
                Name = "Normal"
            });
            context.ColorTypes.Add(new ColorTypeRef
            {
                Id = (long)ColorType.Mat,
                Name = "Matt"
            });
            context.ColorTypes.Add(new ColorTypeRef
            {
                Id = (long)ColorType.Metalic,
                Name = "Metallic"
            });

            context.OrderStatuses.Add(new OrderStatusRef
            {
                Id = (long)OrderStatusId.New,
                Name = "Neu"
            });

            context.OrderStatuses.Add(new OrderStatusRef
            {
                Id = (long)OrderStatusId.Changed,
                Name = "Geändert"
            });

            context.OrderStatuses.Add(new OrderStatusRef
            {
                Id = (long)OrderStatusId.Cancelled,
                Name = "Abgesagt"
            });

            context.OrderStatuses.Add(new OrderStatusRef
            {
                Id = (long)OrderStatusId.Processing,
                Name = "Bearbeitung"
            });

            context.OrderStatuses.Add(new OrderStatusRef
            {
                Id = (long)OrderStatusId.Done,
                Name = "Beendet"
            });

            context.RimTypes.Add(new RimTypeRef
            {
                Id = (long)RimType.Steel,
                Name = "Stall"
            });

            context.RimTypes.Add(new RimTypeRef
            {
                Id = (long)RimType.Alu,
                Name = "Guss"
            });

            context.RimTypes.Add(new RimTypeRef
            {
                Id = (long)RimType.Forged,
                Name = "Geschmiedet"
            });

            context.FuelTypes.Add(new FuelTypeRef
            {
                Id = (long)FuelType.Gas,
                Name = "Benzin"
            });

            context.FuelTypes.Add(new FuelTypeRef
            {
                Id = (long)FuelType.Diesel,
                Name = "Diesel"
            });

            context.SaveChanges();

            #endregion

            #region Equipment

            context.Cars.Add(new Car
            {
                Id = 1,
                Name = "VW Beetle",
                Description = "Volkswagen Beetle",
                Price = 17370,
                Year = 2017,
                ImageUrl = "car_beetle.png"
            });

            context.Cars.Add(new Car
            {
                Id = 2,
                Name = "WV Golf",
                Description = "Volkswagen Golf GTX",
                Price = 25000,
                Year = 2017,
                ImageUrl = "car_golf_gti.png"
            });

            context.Cars.Add(new Car
            {
                Id = 3,
                Name = "WV Passat",
                Description = "Volkswagen Passat",
                Price = 30000,
                Year = 2017,
                ImageUrl = "car_passat.png"
            });

            context.Cars.Add(new Car
            {
                Id = 4,
                Name = "WV Tiguan",
                Description = "Volkswagen Tiguan",
                Price = 40000,
                Year = 2017,
                ImageUrl = "car_tiguan.png"
            });

            context.Cars.Add(new Car
            {
                Id = 5,
                Name = "WV Touareg",
                Description = "Volkswagen Touareg",
                Price = 50000,
                Year = 2017,
                ImageUrl = "car_touareg.png"
            });

            context.Colors.Add(new Color
            {
                Id = 1,
                Name = "Weiß",
                Description = "Einfach weiß",
                TypeId = (long)ColorType.Mat,
                Price = 0,
                Value = "#FFFFFF",
                ImageUrl = "" //todo: seed image url
            });

            context.Colors.Add(new Color
            {
                Id = 2,
                Name = "Metallic Rot",
                Description = "Sportlich und frisch",
                TypeId = (long)ColorType.Metalic,
                Price = 50,
                Value = "#FF0000",
                ImageUrl = "" //todo: seed image url
            });

            context.Colors.Add(new Color
            {
                Id = 3,
                Name = "Metallic Grüßn",
                Description = "Sportlich und frisch",
                TypeId = (long)ColorType.Metalic,
                Price = 50,
                Value = "#00FF00",
                ImageUrl = "" //todo: seed image url
            });

            context.Colors.Add(new Color
            {
                Id = 4,
                Name = "Matt schwarz",
                Description = "Modern",
                TypeId = (long)ColorType.Mat,
                Price = 100,
                Value = "#000000",
                ImageUrl = "" //todo: seed image url
            });

            context.Engines.Add(new Engine
            {
                Id = 1,
                Name = "1.8 TDI (100 PS)",
                Price = 1000,
                Volume = 1.8m,
                Power = 100,
                Description = "Turbodiesel 1.8L 100 PS",
                FuelTypeId = (long)FuelType.Diesel,
                ImageUrl = "engine_diesel.png"
            });

            context.Engines.Add(new Engine
            {
                Id = 2,
                Name = "2.0 TDI (140 PS)",
                Price = 1200,
                Volume = 2.0m,
                Power = 140,
                Description = "Turbodiesel 2.0L 140 PS",
                FuelTypeId = (long)FuelType.Diesel,
                ImageUrl = "engine_diesel.png"
            });

            context.Engines.Add(new Engine
            {
                Id = 3,
                Name = "1.6 MPI (106 PS)",
                Price = 800,
                Volume = 1.6m,
                Power = 106,
                Description = "MPI DOHC 1.6 MPI",
                FuelTypeId = (long)FuelType.Gas,
                ImageUrl = "engine_petrol.png"
            });

            context.Engines.Add(new Engine
            {
                Id = 4,
                Name = "2.5 TFSI (180 PS)",
                Price = 2500,
                Volume = 2.5m,
                Power = 180,
                Description = "TFSI 2.5 (180 PS)",
                FuelTypeId = (long)FuelType.Gas,
                ImageUrl = "engine_petrol.png"
            });

            context.Rims.Add(new Rim
            {
                Id = 1,
                Name = "Stahlfelgen 15\"",
                Description = "Alufelgen 15\"",
                Diameter = 15,
                TypeId = (long)RimType.Steel,
                Price = 0,
                ImageUrl = "wheels_steel.jpg"
            });

            context.Rims.Add(new Rim
            {
                Id = 2,
                Name = "Alufelgen 15\"",
                Description = "Alufelgen 15\"",
                Diameter = 15,
                TypeId = (long)RimType.Alu,
                Price = 100,
                ImageUrl = "wheels_alu.jpg"
            });

            context.Rims.Add(new Rim
            {
                Id = 3,
                Name = "Alufelgen 17\"",
                Description = "Alufelgen 17\"",
                Diameter = 17,
                TypeId = (long)RimType.Alu,
                Price = 150,
                ImageUrl = "wheels_alu.jpg"
            });

            context.Rims.Add(new Rim
            {
                Id = 4,
                Name = "Geschmiedete Felgen 20\"",
                Description = "Geschmiedete Felgen 20\"",
                Diameter = 20,
                TypeId = (long)RimType.Forged,
                Price = 599,
                ImageUrl = "wheels_forged.jpg"
            });

            context.AdditionalEquipmentItems.Add(new AdditionalEquipmentItem
            {
                Id = 1,
                Name = "Ledersessel",
                Description = "Ledersessel",
                Price = 300,
                Category = "Innenraum",
                ImageUrl = "add_skin.jpg"
            });

            context.AdditionalEquipmentItems.Add(new AdditionalEquipmentItem
            {
                Id = 2,
                Name = "ESP",
                Description = "ESP Fahrdynamikregelung",
                Price = 200,
                Category = "Sicherheit",
                ImageUrl = "add_esp.jpg"
            });

            context.AdditionalEquipmentItems.Add(new AdditionalEquipmentItem
            {
                Id = 3,
                Name = "Tempomat",
                Description = "Geschwindigkeitsregelanlage",
                Price = 50,
                Category = "Bequemlichkeit",
                ImageUrl = "add_cruise.jpg"
            });

            context.AdditionalEquipmentItems.Add(new AdditionalEquipmentItem
            {
                Id = 4,
                Name = "Airbag Packet",
                Description = "Zusätzliches Aurbag-Packet",
                Price = 450,
                Category = "Sicherheit",
                ImageUrl = "add_airbag.jpg"
            });

            context.AdditionalEquipmentItems.Add(new AdditionalEquipmentItem
            {
                Id = 5,
                Name = "Klimaanlage",
                Description = "Klimaanlage",
                Price = 399,
                Category = "Bequemlichkeit",
                ImageUrl = "add_climat_control.jpg"
            });

            context.SaveChanges();

            #endregion

            #region Orders

            var orderId1 = Guid.NewGuid();
            var order1 = new Order
            {
                Id = orderId1,
                CarId = 1,
                CustomerName = "Olga Binger",
                EngineId = 1,
                RimId = 1,
                ColorId = 1,
                StatusId = (long)OrderStatusId.New,
            };
            context.Orders.Add(order1);

            var orderId2 = Guid.NewGuid();
            var order2 = new Order
            {
                Id = orderId2,
                CarId = 2,
                CustomerName = "Maksim Binger",
                EngineId = 2,
                RimId = 2,
                ColorId = 2,
                StatusId = (long)OrderStatusId.Done,
            };
            context.Orders.Add(order2);

            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId1,
                EquipmentId = 1
            });
            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId2,
                EquipmentId = 1
            });
            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId2,
                EquipmentId = 2
            });
            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId2,
                EquipmentId = 3
            });
            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId2,
                EquipmentId = 4
            });
            context.OrderAdditionalEquipmentItems.Add(new OrderAdditionalEquipmentItem
            {
                OrderId = orderId2,
                EquipmentId = 5
            });
            context.SaveChanges();

            #endregion
        }
    }
}
