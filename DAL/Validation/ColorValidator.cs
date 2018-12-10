using System;
using System.Collections.Generic;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using DAL.Resource;

namespace DAL.Validation
{
   public class ColorValidator : IEntityValidator
   {
      public List<ValidationEntityError> Validate(IEntity entity, ValidationEntityState state)
      {
         var result = new List<ValidationEntityError>();
         if (state == ValidationEntityState.Added || state == ValidationEntityState.Modified)
         {
            var item = entity as Color;
            if (item != null)
            {
               if (item.Price < 0)
               {
                  result.Add(new ValidationEntityError("Price", Plain.PriceValidationError));
               }
               if (!item.Value.StartsWith("#"))
               {
                  result.Add(new ValidationEntityError("Value", Plain.ColorValueValidationError));
               }
            }
         }
         return result;
      }
   }
}
