using DAL.Common.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DAL.Common.Common
{
    public abstract class LongEntity: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string GetGenericId()
        {
            return Id.ToString(CultureInfo.InvariantCulture);
        }
    }
}
