using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voetbal.BLL.Models;
using Voetbal.BLL.Providers;

namespace Voetbal.Helpers
{
    public static class MatchHelper
    {
        private const Int32 WAITING_TIME = 100;

        public static async Task<Match> ProcessMatch(Match match, MainScreen currentScreen)
        {
            // gather and disseminate players data from both teams
            using (DataProvider dataProvider = new DataProvider())
            {
                IEnumerable<Player> TeamOnePlayers = dataProvider.GetPlayersOfTeam(match.TeamOne.Id);
                IEnumerable<Player> TeamTwoPlayers = dataProvider.GetPlayersOfTeam(match.TeamTwo.Id);

                foreach(Player p in TeamOnePlayers)
                {
                    p.Person = dataProvider.GetPerson(p.PersonId);
                }

                foreach(Player p in TeamTwoPlayers)
                {
                    p.Person = dataProvider.GetPerson(p.PersonId);
                }

                // calculate morale
                float TeamOneMorale = TeamOnePlayers.Average(x => x.Morale);
                float TeamTwoMorale = TeamTwoPlayers.Average(x => x.Morale);

                // calculate defensive stats.
                // Defense stats count twice for defenders, once for midfielders.
                float TeamOneDefense = (((TeamOnePlayers.Where(x => x.Role == PlayerRole.DEFENSE).Sum(x => x.Defense) * 2) + TeamOnePlayers.Where(x => x.Role == PlayerRole.MIDFIELD).Sum(x => x.Defense)) * TeamOneMorale) * (11f / (float)TeamOnePlayers.Count());
                float TeamTwoDefense = (((TeamTwoPlayers.Where(x => x.Role == PlayerRole.DEFENSE).Sum(x => x.Defense) * 2) + TeamTwoPlayers.Where(x => x.Role == PlayerRole.MIDFIELD).Sum(x => x.Defense)) * TeamTwoMorale) * (11f / (float)TeamTwoPlayers.Count());
                
                // calculate attack stats.
                // Attack stats count twice for attackers, once for midfielders.
                float TeamOneAttack = (((TeamOnePlayers.Where(x => x.Role == PlayerRole.ATTACK).Sum(x => x.Attack) * 2) + TeamOnePlayers.Where(x => x.Role == PlayerRole.MIDFIELD).Sum(x => x.Attack)) * TeamOneMorale) * (11f / (float)TeamOnePlayers.Count());
                float TeamTwoAttack = (((TeamTwoPlayers.Where(x => x.Role == PlayerRole.ATTACK).Sum(x => x.Attack) * 2) + TeamTwoPlayers.Where(x => x.Role == PlayerRole.MIDFIELD).Sum(x => x.Attack)) * TeamTwoMorale) * (11f / (float)TeamTwoPlayers.Count());

                // calculate scoring chances based on earlier calculations of both sides attacking/defending strengths.
                Int32 TeamOneChance = (Int32)((TeamOneAttack - TeamTwoDefense) / 8f);
                Int32 TeamTwoChance = (Int32)((TeamTwoAttack - TeamOneDefense) / 8f);

                Panel TeamsPanel = new Panel()
                {
                    Dock = DockStyle.Top,
                    Location = new Point(currentScreen.MainPanel.Width / 2, 60),
                    BorderStyle = BorderStyle.FixedSingle,
                    Height = 300
                };

                StringBuilder TeamOneStringBuilder = new StringBuilder(match.TeamOne.Name);
                TeamOneStringBuilder.AppendLine();
                TeamOneStringBuilder.Append("______");
                TeamOneStringBuilder.AppendLine();

                Int32 P1 = 0;
                foreach (Player p in TeamOnePlayers)
                {
                    P1++;
                    if (P1 == 12)
                    {
                        TeamOneStringBuilder.AppendLine();
                        TeamOneStringBuilder.Append("-----");
                    }
                    TeamOneStringBuilder.AppendLine();
                    TeamOneStringBuilder.Append($"{P1}. {p.Person.FullName}");
                }

                Label TeamOneLabel = new Label()
                {
                    Text = TeamOneStringBuilder.ToString(),
                    TextAlign = ContentAlignment.TopLeft,
                    Location = new Point(0, 10),
                    Dock = DockStyle.Left
                };

                StringBuilder TeamTwoStringBuilder = new StringBuilder(match.TeamTwo.Name);
                TeamTwoStringBuilder.AppendLine();
                TeamTwoStringBuilder.Append("______");
                TeamTwoStringBuilder.AppendLine();

                Int32 P2 = 0;
                foreach (Player p in TeamTwoPlayers)
                {
                    P2++;
                    if (P2 == 12)
                    {
                        TeamTwoStringBuilder.AppendLine();
                        TeamTwoStringBuilder.Append("-----");
                    }
                    TeamTwoStringBuilder.AppendLine();
                    TeamTwoStringBuilder.Append($"{p.Person.FullName} .{P2}");
                }

                Label TeamTwoLabel = new Label()
                {
                    Text = TeamTwoStringBuilder.ToString(),
                    TextAlign = ContentAlignment.TopRight,
                    Location = new Point(0, 10),
                    Dock = DockStyle.Right
                };

                TeamsPanel.Controls.Add(TeamOneLabel);
                TeamsPanel.Controls.Add(TeamTwoLabel);
                currentScreen.MainPanel.Controls.Add(TeamsPanel);

                Boolean skip = false;

                Button skipButton = new Button()
                {
                    Text = "Wedstrijd overslaan",
                    Dock = DockStyle.Bottom,
                    Location = new Point(currentScreen.MainPanel.Width / 2, 60)
                };
                skipButton.Click += (sender, args) =>
                {
                    skip = true;
                };
                currentScreen.MainPanel.Controls.Add(skipButton);

                // create layout prior to match.
                // create top label.
                Label topLabel = new Label()
                {
                    Text = $"{match.TeamOne.Name} - {match.TeamTwo.Name}",
                    TextAlign = ContentAlignment.TopCenter,
                    Dock = DockStyle.Top,
                    Location = new Point(currentScreen.MainPanel.Width / 2, 0)
                };
                currentScreen.MainPanel.Controls.Add(topLabel);

                //create results label.
                Label resultLabel = new Label()
                {
                    Text = "0 - 0",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Location = new Point(currentScreen.MainPanel.Width / 2, 20)
                };
                currentScreen.MainPanel.Controls.Add(resultLabel);


                Label timeLabel = new Label()
                {
                    Text = "0m",
                    TextAlign = ContentAlignment.BottomCenter,
                    Dock = DockStyle.Top,
                    Location = new Point(currentScreen.MainPanel.Width / 2, 40)
                };
                currentScreen.MainPanel.Controls.Add(timeLabel);

                Random rand = new Random();
                // simulate match.
                for (int i = 0; i <= 90 + rand.Next(6); i++)
                {
                    //team one chance
                    Boolean TeamOneChanceThisMinute = rand.Next(65 - TeamOneChance) == 1;
                    Boolean TeamTwoChanceThisMinute = rand.Next(65 - TeamTwoChance) == 1;

                    // prevent both teams scoring in the same minute. one cancels out the other.
                    if (TeamOneChanceThisMinute && !TeamTwoChanceThisMinute)
                    {
                        match.TeamOneGoals++;
                        resultLabel.Text = $"{match.TeamOneGoals} - {match.TeamTwoGoals}";
                    }

                    if (TeamTwoChanceThisMinute && !TeamOneChanceThisMinute)
                    {
                        match.TeamTwoGoals++;
                        resultLabel.Text = $"{match.TeamOneGoals} - {match.TeamTwoGoals}";
                    }

                    timeLabel.Text = $"{i}m";

                    if (!skip)
                    {
                        await Task.Delay(WAITING_TIME);
                    }
                }

                match.Played = true;
                return match;
            }
        }
    }
}
