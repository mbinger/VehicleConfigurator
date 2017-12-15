using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common.Interface;

namespace Service.Common.DataTransfer
{
   public class OrderDto: IDataTransferObject
   {
      public long Id { get; set; }
      public string CustomerName { get; set; }

      public long CarId { get; set; }
      public long EngineId { get; set; }
      public long RimId { get; set; }
      public long ColorId { get; set; }
   }
}
