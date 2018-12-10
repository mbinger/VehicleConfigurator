using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Interface;

namespace DAL.Common.Equipment
{
    /// <summary>
    /// Base car
    /// </summary>
    public class Car : IEntity
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(Attributes.ShortTextLength)]
        public string Name { get; set; }

        [MaxLength(Attributes.LongTextLength)]
        public string Description { get; set; }

        [MaxLength(Attributes.UrlTextLength)]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }
    }
}