using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Interface;
using DAL.Common.Reference;

namespace DAL.Common.Equipment
{
    /// <summary>
    /// Car color
    /// </summary>
    public class Color : IEntity
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

        public virtual ColorTypeRef Type { get; set; }
        public long TypeId { get; set; }

        /// <summary>
        /// Color preview value
        /// </summary>
        [Required, MaxLength(Attributes.ShortTextLength)]
        public string Value { get; set; }
    }
}