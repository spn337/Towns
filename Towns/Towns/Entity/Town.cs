using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towns.Entity
{
    [Table("tblTowns")]
    public class Town
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(maximumLength:255)]
        public string Name { get; set; }

        [ForeignKey("TownType")]
        public int TownTypeId { get; set; }
        public virtual TownType TownType { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

    }
}
