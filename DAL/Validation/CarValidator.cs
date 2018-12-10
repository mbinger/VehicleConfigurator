using System;
using System.Collections.Generic;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using DAL.Resource;

namespace DAL.Validation
{
   public class CarValidator : IEntityValidator
   {
      public List<ValidationEntityError> Validate(IEntity entity, ValidationEntityState state)
      {
         var result = new List<ValidationEntityError>();
         if (state == ValidationEntityState.Added || state == ValidationEntityState.Modified)
         {
            var item = entity as Car;
            if (item != null)
            {
               if (item.Price < 0)
               {
                  result.Add(new ValidationEntityError("Price", Plain.PriceValidationError));
               }
               if (item.Year < 1800)
               {
                  result.Add(new ValidationEntityError("Year", Plain.CarYearValidationError));
               }
            }
         }
         return result;
      }
   }
}
