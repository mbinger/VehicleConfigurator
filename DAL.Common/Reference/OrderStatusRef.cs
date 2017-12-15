using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Interface;

namespace DAL.Common.Reference
{
   public class OrderStatusRef: IEntity
   {
      public long Id { get; set; }

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
