﻿using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using DAL.Common.Reference;

namespace DAL.Common.Booking
{
    public class Order: IEntity
    {
        public Order()
        {
            DateCreatedUtc = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public Guid Key { get; set; }

        [Required, MaxLength(Attributes.LongTextLength)]
        public string CustomerName { get; set; }

        public DateTime DateCreatedUtc { get; set; }
        public DateTime? DateChangedUtc { get; set; }

        public virtual OrderStatusRef Status { get; set; }
        public long StatusId { get; set; }

        public virtual Car Car { get; set; }
        public long CarId { get; set; }

        public virtual Engine Engine { get; set; }
        public long EngineId { get; set; }

        public virtual Color Color { get; set; }
        public long ColorId { get; set; }

        public virtual Rim Rim { get; set; }
        public long RimId { get; set; }
    }
}
