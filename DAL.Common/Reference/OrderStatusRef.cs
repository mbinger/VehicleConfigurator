using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Common;

namespace DAL.Common.Reference
{
   public class OrderStatusRef: LongEntity
    {

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }
   }

   public enum OrderStatusId
   {
      New = 1,
      Changed = 2,
      Cancelled = 3,
      Processing = 4,
      Done = 5
   }
}
