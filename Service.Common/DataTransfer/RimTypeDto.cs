using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common.Interface;

namespace Service.Common.DataTransfer
{
   public class RimTypeDto : IDataTransferObject
   {
      public long Id { get; set; }
      public string Name { get; set; }
   }
}
