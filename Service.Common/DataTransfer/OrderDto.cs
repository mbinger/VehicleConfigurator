using System;
using Service.Common.Interface;

namespace Service.Common.DataTransfer
{
   public class OrderDto: IDataTransferObject
   {
      public Guid Id { get; set; }
      public string CustomerName { get; set; }

      public long CarId { get; set; }
      public long EngineId { get; set; }
      public long RimId { get; set; }
      public long ColorId { get; set; }
   }
}
