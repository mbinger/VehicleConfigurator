using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common.Interface.Validation
{
   /// <summary>
   /// Entity validation error
   /// </summary>
   public class ValidationEntityError
   {
      public ValidationEntityError(string property, string error)
      {
         Property = property;
         Error = error;
      }
      public string Property { get; set; }
      public string Error { get; set; }
   }
}
