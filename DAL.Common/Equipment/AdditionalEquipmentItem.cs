using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Interface;

namespace DAL.Common.Equipment
{
   public class AdditionalEquipmentItem : IEntity
   {
      public long Id { get; set; }

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }

      [MaxLength(Attributes.LongTextLength)]
      public string Description { get; set; }

      [MaxLength(Attributes.UrlTextLength)]
      public string ImageUrl { get; set; }

      public decimal Price { get; set; }

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Category { get; set; }
   }
}
