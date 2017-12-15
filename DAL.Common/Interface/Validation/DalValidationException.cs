using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common.Interface.Validation
{
   /// <summary>
   /// Validation exception
   /// </summary>
   public class DalValidationException : Exception
   {
      public DalValidationException()
      {
         Errors = new List<ValidationEntityError>();
      }

      public List<ValidationEntityError> Errors { get; set; }

      public override string Message
      {
         get { return String.Join("\n", Errors.Select(p => String.Format("{0}:{1}", p.Property, p.Error))); }
      }
   }
}
