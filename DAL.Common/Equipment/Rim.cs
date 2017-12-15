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
   /// Rim item
   /// </summary>
   public class Rim : IEntity
   {
      [Key]
      public long Id { get; set; }

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }

      [MaxLength(Attributes.LongTextLength)]
      public string Description { get; set; }

      [MaxLength(Attributes.UrlTextLength)]
      public string ImageUrl { get; set; }

      public decimal Price { get; set; }

      public virtual RimTypeRef Type { get; set; }
      public long TypeId { get; set; }

      /// <summary>
      /// Rim diameter
      /// </summary>
      public decimal Diameter { get; set; }
   }
}
