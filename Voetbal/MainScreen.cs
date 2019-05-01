using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voetbal.BLL.Models;
using Voetbal.BLL.Providers;
using Voetbal.Helpers;

namespace Voetbal
{
    public partial class MainScreen : Form
    {
        private const float YIELD_TIME = 0.5f; //half a second.
        private TournamentGroup currentTournament;
        private Int32 nextMatchIndex = 0;

        public MainScreen()
        {
            InitializeComponent();
        }

        private void PopulatePanelRankings()
        {
            this.MainPanel.Controls.Clear();
            DataTable RankingTable = TableHelper.CreateRankingsTable(currentTournament);
            DataGrid dataGrid = new DataGrid
            {
                CaptionText = "Rankings",
                DataSource = RankingTable,
                Dock = DockStyle.Fill                
            };
            
            this.MainPanel.Controls.Add(dataGrid);
            if (nextMatchIndex == currentTournament.Matches.Count)
            {
                this.matchDataLabel.Text = $"Het toernooi is afgelopen!";
                this.SimulateNextMatchButton.Enabled = false;
                this.ProcessButton.Enabled = true;
            }
            else
            {
                this.matchDataLabel.Text = $"Volgende wedstrijd is: {currentTournament.Matches[nextMatchIndex].TeamOne.Name} - {currentTournament.Matches[nextMatchIndex].TeamTwo.Name}";
                this.SimulateNextMatchButton.Enabled = true;
            }
        }

        private async Task PopulatePanelMatch()
        {
            this.MainPanel.Controls.Clear();
            this.SimulateNextMatchButton.Enabled = false;
            currentTournament.Matches[nextMatchIndex] = await MatchHelper.ProcessMatch(currentTournament.Matches[nextMatchIndex], this);
            nextMatchIndex++;
            this.SimulateNextMatchButton.Enabled = true;
            PopulatePanelRankings();
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            // clear mainpanel
            this.MainPanel.Controls.Clear();
            this.SimulateNextMatchButton.Enabled = true;
            this.ProcessButton.Enabled = false;
            

            // load data
            this.MainPanel.Controls.Add(new Label() { Text = "Data word geladen." });


            // simulate tournament.
            using (DataProvider dataProvider = new DataProvider())
            {
                currentTournament = new TournamentGroup
                {
                    Teams = dataProvider.GetTeams().ToList(),
                    Matches = new List<Match>()
                };

                // set up the matches that need to happen:
                // A-B, C-D, A-C, B-D, D-A, C-B
                currentTournament.Matches.Add(new Match(currentTournament.Teams[0], currentTournament.Teams[1]));
                currentTournament.Matches.Add(new Match(currentTournament.Teams[2], currentTournament.Teams[3]));
                currentTournament.Matches.Add(new Match(currentTournament.Teams[0], currentTournament.Teams[2]));
                currentTournament.Matches.Add(new Match(currentTournament.Teams[1], currentTournament.Teams[3]));
                currentTournament.Matches.Add(new Match(currentTournament.Teams[3], currentTournament.Teams[0]));
                currentTournament.Matches.Add(new Match(currentTournament.Teams[2], currentTournament.Teams[1]));

                // populate panel.
                PopulatePanelRankings();
            }
        }

        private async void SimulateNextMatchButton_Click(object sender, EventArgs e)
        {
            await PopulatePanelMatch();
        }
    }
}
