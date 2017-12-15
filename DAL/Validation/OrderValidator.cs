using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Booking;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Interface.Validation;
using DAL.Resource;

namespace DAL.Validation
{
   public class OrderValidator : IEntityValidator
   {
      public List<ValidationEntityError> Validate(object entity, ValidationEntityState state)
      {
         var result = new List<ValidationEntityError>();
         if (state == ValidationEntityState.Added || state == ValidationEntityState.Modified)
         {
            var item = entity as Order;
            if (item != null)
            {
               if (String.Compare(item.CustomerName, "Napoleon", StringComparison.InvariantCultureIgnoreCase) == 0)
               {
                  result.Add(new ValidationEntityError("CustomerName", String.Format(Plain.CouldNotHaveCar, item.CustomerName)));
               }
            }
         }
         return result;
      }
   }
}
