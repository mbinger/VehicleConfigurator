using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Interface.Validation;

namespace DAL.Common.Interface
{
   /// <summary>
   /// Entity validator
   /// </summary>
   public interface IEntityValidator
   {
      /// <summary>
      /// Validate an entity
      /// </summary>
      /// <param name="entity">Entity object</param>
      /// <param name="state">Entity state</param>
      /// <returns>List of errors or null/empty if ok</returns>
      List<ValidationEntityError> Validate(object entity, ValidationEntityState state);
   }
}
