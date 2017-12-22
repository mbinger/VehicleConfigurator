using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Common;

namespace DAL.Common.Reference
{
   public class RimTypeRef: LongEntity
   {

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }
   }
}
