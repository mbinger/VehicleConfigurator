using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common.Interface;

namespace Service.Common.DataTransfer
{
   public class RimDto: IDataTransferObject
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string ImageUrl { get; set; }
      public decimal Price { get; set; }
      public RimTypeDto Type { get; set; }
      public long TypeId { get; set; }
      public decimal Diameter { get; set; }
   }
}
