using DAL.Common.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Common.Common
{
    public abstract class GuidEntity: IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string GetGenericId()
        {
            return Id.ToString();
        }
    }
}
