using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Interface;
using DAL.Common.Reference;
namespace DAL.Common.Equipment
{
   /// <summary>
   /// Engine
   /// </summary>
   public class Engine : IEntity
   {
      public Engine()
      {
         Volume = 1;
         Power = 60;
      }

      public long Id { get; set; }

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }

      [MaxLength(Attributes.LongTextLength)]
      public string Description { get; set; }

      [MaxLength(Attributes.UrlTextLength)]
      public string ImageUrl { get; set; }

      public decimal Price { get; set; }

      public virtual FuelTypeRef FuelType { get; set; }
      public long FuelTypeId { get; set; }

      /// <summary>
      /// Engine volume in L
      /// </summary>
      public decimal Volume { get; set; }

      /// <summary>
      /// Engine power PS
      /// </summary>
      public decimal Power { get; set; }
   }
}
