using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    [Table("Coach")]
    public class Coach : IDatabaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Guid PersonId { get; set; }

        [Column("TeamId")]
        public Guid TeamId { get; set; }

        [Ignore]
        public Person Person { get; set; }

        [Column("Leadership")]
        public float Leadership { get; set; } // improves players morale during matches, or lowers it.

        [Column("Strategy")]
        public float Strategy { get; set; } // modifies players effectiveness.
    }
}
