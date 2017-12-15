using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common;
using Service.Common.Interface;

namespace Service.Common.DataTransfer
{
   public class EngineDto: IDataTransferObject
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public string ImageUrl { get; set; }
      public decimal Price { get; set; }
      public decimal Volume { get; set; }
      public decimal Power { get; set; }
      public FuelTypeDto FuelType { get; set; }
      public long FuelTypeId { get; set; }
   }
}
