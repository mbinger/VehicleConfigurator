using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.DataTransfer
{
   public class CudResultDto
   {
      public CudResultDto()
      {
         Errors = new List<string>();
      }
      public string Id { get; set; }
      public bool Success { get; set; }
      public List<string> Errors { get; set; }
   }
}
