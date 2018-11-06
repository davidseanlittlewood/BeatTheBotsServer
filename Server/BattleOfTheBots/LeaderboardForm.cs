using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BattleOfTheBots.UIControl;
using BattleOfTheBots.Logic;
using System.Threading;
using BattleOfTheBots.State;

namespace BattleOfTheBots
{    

    public partial class LeaderboardForm : Form
    {
        BotsUI botUI = new BotsUI();
        private string leaderboardConfigFile = "leaderboard.xml";

        public LeaderboardForm()
        {
            InitializeComponent();
            panelBotUIDock.Controls.Add(botUI);
            botUI.Dock = DockStyle.Fill;

        }

        public void Clear()
        {
            dtLeaderBoard.Rows.Clear();
            gridLeaderboard.DataSource = dtLeaderBoard;
            gridLeaderboard.Refresh();
        }

        public delegate void UpdateCurrentMatchdelegate(Arena arena, string matchDetails, BotMove botA, BotMove botB);

        public void UpdateCurrentMatch(Arena arena, string matchDetails, BotMove leftBot, BotMove rightBot)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurrentMatchdelegate(UpdateCurrentMatch), arena, matchDetails, leftBot, rightBot);
            }
            else
            {                            
                for (int i = 1; i <= 3; i++)
                {

                    lblCurrentMatch.Text = matchDetails;
                    lblCurrentMatch.Refresh();

                    botUI.Update(arena.NumberOfSquares, leftBot, rightBot, i);
                    

                    /* Set short sleep and process messages in a loop 
                       to stop Thread.sleep blocking UI
                     */                    
                    for (var x=0; x<10; x++)
                    {
                        Thread.Sleep(10); 
                        Application.DoEvents();
                    }
                }
            }
        }


        public delegate void RegisterDrawDelegate();

        public void RegisterDraw()
        {
            if (InvokeRequired)
            {
                Invoke(new RegisterDrawDelegate(RegisterDraw));
            }
            else
            {
                botUI.WriteReallyBigText("Draw!");
            }
        }

        public delegate void RegisterBotWinDelegate(Bot winner, Bot loser, VictoryType victoryType);

        public void RegisterBotWin(Bot winner, Bot loser, VictoryType victoryType)
        {

            if (InvokeRequired)
            {
                Invoke(new RegisterBotWinDelegate(RegisterBotWin), winner, loser, victoryType);
            }
            else
            {
                DataRow botRow;
                DataRow[] rows = dtLeaderBoard.Select(string.Format("Name = '{0}'", winner.Name));
                if (rows.Length > 0)
                {
                    botRow = rows.First();
                    botRow["Wins"] = (Int32)botRow["Wins"] + 1;
                }
                else
                {
                    botRow = dtLeaderBoard.NewRow();
                    botRow["Name"] = winner.Name;
                    botRow["Wins"] = 1;

                    dtLeaderBoard.Rows.Add(botRow);
                }

                SortResults();
                gridLeaderboard.Refresh();

                dtLeaderBoard.WriteXml(leaderboardConfigFile);
            }

            var gloatText = GetGloatText(winner, loser, victoryType);
            botUI.WriteReallyBigText($"{winner.Name} wins!{Environment.NewLine}{gloatText}");
        }

        private string GetGloatText(Bot winner, Bot loser, VictoryType victoryType)
        {            
            switch (victoryType)
            {
                case VictoryType.OutOfBounds:
                    return GetOutOfBoundsGloat(winner, loser);
                case VictoryType.ReducedToZeroHealth:
                    return GetZeroHeathGloat(winner, loser);
                case VictoryType.GivenOnDamage:
                    return $"{winner.Name} inflicted more damage";
                case VictoryType.GivenOnProgress:
                    return $"{winner.Name} made more progress";
                default:
                    return null;
            }
        }

        private static string GetZeroHeathGloat(Bot winner, Bot loser)
        {
            var rand = new Random();
            switch (rand.Next(0, 5))
            {
                case 0:
                    return $"{loser.Name} needs a screwdriver";
                case 1:
                    return $"{loser.Name} is spare parts";
                case 2:
                    return $"Does anyone have a spanner?";
                case 3:
                    return $"{loser.Name} is considering a career as a doorstop";
                default:
                    return "Knock Out";
            }
            
        }

        private string GetOutOfBoundsGloat(Bot winner, Bot loser)
        {
            var rand = new Random();
            switch (rand.Next(0, 4))
            {
                case 0:
                    return $"{loser.Name} went for a swim";
                case 1:
                    return $"{loser.Name} doesn't like the water...";
                case 2:
                    return $"{loser.Name} makes a better anchor than a boat";
                default:
                    return $"{loser.Name} swam like a brick";
            }
        }

        public delegate void SortResultsDelegate();
        public void SortResults()
        {

            if (InvokeRequired)
            {
                Invoke(new SortResultsDelegate(SortResults));
            }
            else
            {
                if (!this.Visible)
                    return;

                DataView dv = dtLeaderBoard.DefaultView;
                dv.Sort = "Wins desc";
                dtLeaderBoard = dv.ToTable("Leaderboard");
                gridLeaderboard.DataSource = dtLeaderBoard;
            }
        }

        private void LeaderboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void LoadConfiguration()
        {
            if (File.Exists(leaderboardConfigFile))
            {
                dtLeaderBoard.Rows.Clear();
                dtLeaderBoard.ReadXml(leaderboardConfigFile);
                gridLeaderboard.Refresh();
            }
            
        }

    }
}
