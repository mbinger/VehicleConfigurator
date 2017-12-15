using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using DAL.Common.Interface;

namespace DAL.Common.Reference
{
   public class ColorTypeRef: IEntity
   {
      public long Id { get; set; }

      [Required, MaxLength(Attributes.ShortTextLength)]
      public string Name { get; set; }
   }
}
