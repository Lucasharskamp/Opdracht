using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    [Table("Person")]
    public class Person : IDatabaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("FirstName")]
        public String FirstName { get; set; }

        [Column("LastName")]
        public String LastName { get; set; }

        [Ignore]
        public String FullName => $"{FirstName} {LastName}";        
    }
}
