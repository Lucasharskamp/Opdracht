using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    public class Match
    {
        public Boolean Played { get; set; } = false;

        public Team TeamOne { get; set; }

        public Team TeamTwo { get; set; }

        public Int32 TeamOneGoals { get; set; }

        public Int32 TeamTwoGoals { get; set; }

        public Match (Team TeamOne, Team TeamTwo)
        {
            this.TeamOne = TeamOne;
            this.TeamTwo = TeamTwo;
        }
    }
}
