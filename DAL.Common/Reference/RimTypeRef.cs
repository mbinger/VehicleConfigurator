using System;
using System.ComponentModel.DataAnnotations;
using DAL.Common.Interface;

namespace DAL.Common.Reference
{
    public class RimTypeRef : IEntity
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(Attributes.ShortTextLength)]
        public string Name { get; set; }
    }
}