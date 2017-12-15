using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Linq;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using DAL.Common.Reference;

namespace DAL.Context
{
   public class ConfigDbContext : DbContext
   {

      private const string DefaultConnectionName = "DefaultConnection";

      /// <summary>
      /// default
      /// </summary>
      public ConfigDbContext()
         : base(DefaultConnectionName)
      {
      }

      /// <summary>
      /// with validation support
      /// </summary>
      /// <param name="serviceLocator"></param>
      public ConfigDbContext(IServiceLocator serviceLocator):
         base(DefaultConnectionName)
      {
         _serviceLocator = serviceLocator;
      }

      private IServiceLocator _serviceLocator;

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

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         #region Booking

         modelBuilder.Entity<Order>().HasKey(p => p.Id);
         modelBuilder.Entity<Order>()
            .HasRequired(p => p.Car)
            .WithMany()
            .HasForeignKey(p => p.CarId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Order>()
            .HasRequired(p => p.Engine)
            .WithMany()
            .HasForeignKey(p => p.EngineId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Order>()
            .HasRequired(p => p.Rim)
            .WithMany()
            .HasForeignKey(p => p.RimId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Order>()
            .HasRequired(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Order>()
            .HasRequired(p => p.Color)
            .WithMany()
            .HasForeignKey(p => p.ColorId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<OrderAdditionalEquipmentItem>().HasKey(p => p.Id);
         modelBuilder.Entity<OrderAdditionalEquipmentItem>()
            .HasRequired(p => p.Order)
            .WithMany()
            .HasForeignKey(p => p.OrderId)
            .WillCascadeOnDelete(true);

         modelBuilder.Entity<OrderAdditionalEquipmentItem>()
            .HasRequired(p => p.EquipmentItem)
            .WithMany()
            .HasForeignKey(p => p.EquipmentId)
            .WillCascadeOnDelete(false);

         #endregion

         #region Equipment

         modelBuilder.Entity<AdditionalEquipmentItem>().HasKey(p => p.Id);
         modelBuilder.Entity<Car>().HasKey(p => p.Id);

         modelBuilder.Entity<Color>().HasKey(p => p.Id);
         modelBuilder.Entity<Color>()
            .HasRequired(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Rim>().HasKey(p => p.Id);
         modelBuilder.Entity<Rim>()
            .HasRequired(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId)
            .WillCascadeOnDelete(false);

         modelBuilder.Entity<Engine>().HasKey(p => p.Id);
         modelBuilder.Entity<Engine>()
            .HasRequired(p => p.FuelType)
            .WithMany()
            .HasForeignKey(p => p.FuelTypeId)
            .WillCascadeOnDelete(false);

         #endregion

         #region Reference

         modelBuilder.Entity<ColorTypeRef>().HasKey(p => p.Id);
         modelBuilder.Entity<FuelTypeRef>().HasKey(p => p.Id);
         modelBuilder.Entity<OrderStatusRef>().HasKey(p => p.Id);
         modelBuilder.Entity<RimTypeRef>().HasKey(p => p.Id);

         #endregion

      }

      /// <summary>
      /// Do custom entity validation
      /// </summary>
      /// <param name="entityEntry"></param>
      /// <param name="items"></param>
      /// <returns></returns>
      protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<Object, Object> items)
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
                  validationException.Errors.Add(new ValidationEntityError(dbValidationError.PropertyName,  dbValidationError.ErrorMessage));
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
      }
   }
}
