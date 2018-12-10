using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Interface;

namespace DAL.Common.Reference
{
    public class OrderStatusRef : IEntity
    {
        [Key]
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
