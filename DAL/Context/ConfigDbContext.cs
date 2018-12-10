using System;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Reference;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext()
        {
        }

        public ConfigDbContext(DbContextOptions options):base(options)
        {
        }

        #region References

        public DbSet<ColorTypeRef> ColorTypes { get; set; }
        public DbSet<OrderStatusRef> OrderStatuses { get; set; }
        public DbSet<RimTypeRef> RimTypes { get; set; }
        public DbSet<FuelTypeRef> FuelTypes { get; set; }

        #endregion

        #region Equipment

        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Rim> Rims { get; set; }
        public DbSet<AdditionalEquipmentItem> AdditionalEquipmentItems { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAdditionalEquipmentItem> OrderAdditionalEquipmentItems { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Booking

            modelBuilder.Entity<Order>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>().HasIndex(p => p.Key).IsUnique();

            modelBuilder.Entity<Order>()
               //todo:.HasOne(p => p.Car)
               .HasOne(p => p.Car)
               .WithMany()
               .HasForeignKey(p => p.CarId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(p => p.Engine)
               .WithMany()
               .HasForeignKey(p => p.EngineId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(p => p.Rim)
               .WithMany()
               .HasForeignKey(p => p.RimId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(p => p.Status)
               .WithMany()
               .HasForeignKey(p => p.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(p => p.Color)
               .WithMany()
               .HasForeignKey(p => p.ColorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderAdditionalEquipmentItem>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderAdditionalEquipmentItem>()
               .HasOne(p => p.Order)
               .WithMany()
               .HasForeignKey(p => p.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderAdditionalEquipmentItem>()
               .HasOne(p => p.EquipmentItem)
               .WithMany()
               .HasForeignKey(p => p.EquipmentId)
               .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Equipment

            modelBuilder.Entity<AdditionalEquipmentItem>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Car>().Property(p=>p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Color>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Color>()
               .HasOne(p => p.Type)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rim>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Rim>()
               .HasOne(p => p.Type)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Engine>().Property(p=>p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Engine>()
               .HasOne(p => p.FuelType)
               .WithMany()
               .HasForeignKey(p => p.FuelTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Reference

            modelBuilder.Entity<ColorTypeRef>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<FuelTypeRef>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderStatusRef>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RimTypeRef>().Property(p => p.Id).ValueGeneratedOnAdd();

            #endregion

        }

        /*todo: validation?
        /// <summary>
        /// Do custom entity validation
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected override DbEntityValidationResult ValidateEntity(DEntityEntry entityEntry, IDictionary<Object, Object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);

            if (_serviceLocator != null && entityEntry != null && entityEntry.Entity != null)
            {
                //resolve validation provider

                IEntityValidator validationProvider = null;

                var entityProxyType = entityEntry.Entity.GetType();
                if (entityProxyType.BaseType != null)
                {
                    validationProvider = _serviceLocator.GetNamedServiceSafe<IEntityValidator>(entityProxyType.BaseType == typeof(Object)
                       ? entityProxyType
                       : entityProxyType.BaseType);
                }

                if (validationProvider != null)
                {
                    ValidationEntityState state;
                    if (entityEntry.State.HasFlag(EntityState.Added))
                    {
                        state = ValidationEntityState.Added;
                    }
                    else if (entityEntry.State.HasFlag(EntityState.Modified))
                    {
                        state = ValidationEntityState.Modified;
                    }
                    else if (entityEntry.State.HasFlag(EntityState.Deleted))
                    {
                        state = ValidationEntityState.Deleted;
                    }
                    else
                    {
                        state = ValidationEntityState.Othder;
                    }

                    var validationSummary = validationProvider.Validate(entityEntry.Entity, state);
                    if (validationSummary != null && validationSummary.Any())
                    {
                        foreach (var item in validationSummary)
                        {
                            result.ValidationErrors.Add(new DbValidationError(item.Property, item.Error));
                        }
                    }
                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //re-throw validation exception
                var validationException = new DalValidationException();
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    foreach (var dbValidationError in validationError.ValidationErrors)
                    {
                        validationException.Errors.Add(new ValidationEntityError(dbValidationError.PropertyName, dbValidationError.ErrorMessage));
                    }
                }

                throw validationException;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                var disposableLocator = _serviceLocator as IDisposable;
                if (disposableLocator != null)
                {
                    disposableLocator.Dispose();
                }
                _serviceLocator = null;
            }
        }*/
    }
}