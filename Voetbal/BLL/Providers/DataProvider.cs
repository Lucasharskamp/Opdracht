using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voetbal.Helpers;
using Voetbal.BLL.Models;

namespace Voetbal.BLL.Providers
{
    public class DataProvider : ProviderBase
    {
        public DataProvider() : base(CommandsHelper.GetDatabaseConnection()) { }

        public IEnumerable<Team> GetTeams()
        {
            return Database.Query<Team>("SELECT * FROM Team LIMIT 4");
        }

        public IEnumerable<Player> GetPlayersOfTeam(Guid TeamId)
        {
            return Database.Query<Player>("SELECT * FROM Player WHERE TeamId = ?", TeamId);
        }

        public Person GetPerson(Guid id)
        {
            return Database.FindWithQuery<Person>("SELECT * FROM Person WHere Id = ?",id);
        }
    }
}
