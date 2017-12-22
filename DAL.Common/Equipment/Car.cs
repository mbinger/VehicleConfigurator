using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Common;
using DAL.Common.Interface;

namespace DAL.Common.Equipment
{
   /// <summary>
   /// Base car
   /// </summary>
   public class Car : LongEntity
   {
      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }

      [MaxLength(Attributes.LongTextLength)]
      public string Description { get; set; }

      [MaxLength(Attributes.UrlTextLength)]
      public string ImageUrl { get; set; }

      public decimal Price { get; set; }

      public int Year { get; set; }
   }
}
