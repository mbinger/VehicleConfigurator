using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Equipment;
using DAL.Common.Interface;

namespace DAL.Common.Booking
{
    public class OrderAdditionalEquipmentItem : IEntity
    {
        [Key]
        public long Id { get; set; }

        public virtual Order Order { get; set; }
        public long OrderId { get; set; }

        public virtual AdditionalEquipmentItem EquipmentItem { get; set; }
        public long EquipmentId { get; set; }
    }
}
