using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common.Interface
{
   /// <summary>
   /// Service locator
   /// </summary>
   public interface IServiceLocator
   {
      /// <summary>
      /// Get service by type
      /// </summary>
      /// <returns></returns>
      T GetService<T>() where T : class;

      /// <summary>
      /// Get named service if registered
      /// </summary>
      /// <typeparam name="T">Service type</typeparam>
      /// <returns>Instance or null if not found</returns>
      T GetNamedServiceSafe<T>(Type kind) where T : class;
   }
}
