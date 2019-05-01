using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voetbal.BLL.Models
{
    public class TournamentGroup
    {
        public List<Team> Teams { get; set; }

        public List<Match> Matches { get; set; }
    }
}
