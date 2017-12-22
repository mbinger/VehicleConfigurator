using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Common;

namespace DAL.Common.Reference
{
   public class FuelTypeRef: LongEntity
    {
      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }
   }
}
