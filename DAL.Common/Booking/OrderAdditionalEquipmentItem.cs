using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Common.Equipment;
using DAL.Common.Interface;

namespace DAL.Common.Booking
{
   public class OrderAdditionalEquipmentItem: IEntity
   {
      public long Id { get; set; }

      public virtual Order Order { get; set; }
      public long OrderId { get; set; }

      public virtual AdditionalEquipmentItem EquipmentItem { get; set; }
      public long EquipmentId { get; set; }
   }
}
