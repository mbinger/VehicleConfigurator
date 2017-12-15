using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Reference;

namespace DAL.Common.Booking
{
   public class Order: IEntity
   {
      public Order()
      {
         DateCreatedUtc = DateTime.UtcNow;
      }
      public long Id { get; set; }

      [Required, MaxLength(Attributes.LongTextLength)]
      public string CustomerName { get; set; }

      public DateTime DateCreatedUtc { get; set; }
      public DateTime? DateChangedUtc { get; set; }

      public virtual OrderStatusRef Status { get; set; }
      public long StatusId { get; set; }

      public virtual Car Car { get; set; }
      public long CarId { get; set; }

      public virtual Engine Engine { get; set; }
      public long EngineId { get; set; }

      public virtual Color Color { get; set; }
      public long ColorId { get; set; }

      public virtual Rim Rim { get; set; }
      public long RimId { get; set; }
   }
}
