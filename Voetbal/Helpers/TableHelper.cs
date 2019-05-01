using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voetbal.BLL.Models;
using Voetbal.BLL.Providers;

namespace Voetbal.Helpers
{
    public static class TableHelper
    {
        private struct Result
        {
            public Team team;
            public Int32 won;
            public Int32 draw;
            public Int32 lost;
            public Int32 points;

            public Int32 goalsFor;
            public Int32 goalsAgainst;
            public Int32 goalsRatio;
        }

        public static DataTable CreateRankingsTable(TournamentGroup group)
        {
            DataTable table = new DataTable("RankingsTable");

            // Create first column and add to the DataTable.
            DataColumn rankingColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "Positie",
                AutoIncrement = true,
                Caption = "Positie",
                ReadOnly = true,
                Unique = true
            };
            table.Columns.Add(rankingColumn);


            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["Positie"];
            table.PrimaryKey = PrimaryKeyColumns;


            DataColumn NameColumn = new DataColumn
            {
                DataType = typeof(String),
                ColumnName = "Name",
                Caption = "Naam",
                ReadOnly = true,
                Unique = true                
            };
            table.Columns.Add(NameColumn);

            DataColumn MatchesWonAmount = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "W",
                Caption = "W",
                ReadOnly = true,
                Unique = false
            };
            table.Columns.Add(MatchesWonAmount);

            DataColumn MatchesDrawAmount = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "G",
                Caption = "G",
                ReadOnly = true,
                Unique = false
            };
            table.Columns.Add(MatchesDrawAmount);

            DataColumn MatchesLostAmount = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "V",
                Caption = "V",
                ReadOnly = true,
                Unique = false
            };
            table.Columns.Add(MatchesLostAmount);

            DataColumn PointsAmount = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "Punten",
                Caption = "Punten",
                ReadOnly = true,
                Unique = false
            };
            table.Columns.Add(PointsAmount);

            DataColumn Goals = new DataColumn
            {
                DataType = typeof(String),
                ColumnName = "Doelsaldo",
                Caption = "Doelsaldo",
                ReadOnly = true,
                Unique = false
            };
            table.Columns.Add(Goals);

            // calculate table data.
            List<Result> results = new List<Result>();
            foreach(Team team in group.Teams)
            {
                Result thisTeamResult = new Result
                {
                    team = team
                };

                // calculate results data.
                IEnumerable<Match> homeMatches = group.Matches.Where(x => x.Played && x.TeamOne == team);
                foreach(Match m in homeMatches)
                {
                    thisTeamResult.goalsFor += m.TeamOneGoals;
                    thisTeamResult.goalsAgainst += m.TeamTwoGoals;

                    Int32 difference = m.TeamOneGoals - m.TeamTwoGoals;
                    if (difference < 0) { thisTeamResult.lost++; } else
                    if (difference == 0) { thisTeamResult.draw++; thisTeamResult.points++; } else
                    if (difference > 0) { thisTeamResult.won++; thisTeamResult.points += 3; }
                }

                IEnumerable<Match> againstMatches = group.Matches.Where(x => x.Played && x.TeamTwo == team);
                foreach (Match m in againstMatches)
                {
                    thisTeamResult.goalsFor += m.TeamTwoGoals;
                    thisTeamResult.goalsAgainst += m.TeamOneGoals;

                    Int32 difference = m.TeamTwoGoals - m.TeamOneGoals;
                    if (difference < 0) { thisTeamResult.lost++; } else
                    if (difference == 0) { thisTeamResult.draw++; thisTeamResult.points++; } else
                    if (difference > 0) { thisTeamResult.won++; thisTeamResult.points += 3; }
                }
                thisTeamResult.goalsRatio = thisTeamResult.goalsFor - thisTeamResult.goalsAgainst;
                results.Add(thisTeamResult);
            }

            //populate the table. order priority; goals, ratio, mostgoals, matches won.
            results = results.OrderByDescending(x => x.won)
                             .OrderByDescending(x => x.goalsFor)
                             .OrderByDescending(x => x.goalsRatio)
                             .OrderByDescending(x => x.points).ToList();

            Int32 i = 0;
            foreach(Result r in results)
            {
                i++;
                DataRow row = table.NewRow();
                row["Positie"] = i;
                row["Name"] = r.team.Name;
                row["W"] = r.won;
                row["G"] = r.draw;
                row["V"] = r.lost;
                row["Punten"] = r.points;
                row["Doelsaldo"] = $"{r.goalsFor} - {r.goalsAgainst} ({r.goalsRatio})";

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
