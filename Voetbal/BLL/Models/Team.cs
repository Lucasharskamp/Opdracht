using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    [Table("Team")]
    public class Team : IDatabaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Name")]
        public String Name { get; set; }
    }
}
