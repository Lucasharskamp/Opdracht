
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    public enum PlayerRole
    {
        GOALKEEP,
        DEFENSE,
        MIDFIELD,
        ATTACK
    }

    [Table("Player")]
    public class Player : IDatabaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Guid PersonId { get; set; }

        [Column("TeamId")]
        public Guid TeamId { get; set; }

        [Ignore]
        public Person Person { get; set; }

        public PlayerRole Role;

        [Column("PlayerRole")]
        public String PlayerRole
        {
            get => Enum.GetName(typeof(PlayerRole), Role);
            set => Role = (PlayerRole)Enum.Parse(typeof(PlayerRole), value); 
        }

        [Column("Defense")]
        public float Defense { get; set; }
        
        [Column("Attack")]
        public float Attack { get; set; }

        [Column("Morale")]
        public float Morale { get; set; }

    }
}
