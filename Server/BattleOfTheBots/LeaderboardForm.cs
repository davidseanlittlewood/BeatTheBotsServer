using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BattleOfTheBots.UIControl;
using BattleOfTheBots.Logic;
using System.Threading;
using BattleOfTheBots.State;
using BattleOfTheBots.Classes;
using System.Collections.Generic;

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
                        Thread.Sleep(5); 
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

        public delegate void RegisterBotWinDelegate(Bot winner, Bot loser, VictoryType victoryType, Options options);

        public void RegisterBotWin(Bot winner, Bot loser, VictoryType victoryType, Options options)
        {

            if (InvokeRequired)
            {
                Invoke(new RegisterBotWinDelegate(RegisterBotWin), winner, loser, victoryType, options);
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

                SaveLeaderboard();

                var gloatText = GetGloatText(winner, loser, victoryType, options);
                botUI.WriteReallyBigText($"{winner.Name} wins!{Environment.NewLine}{gloatText}");
            }
        }

        public void SaveLeaderboard()
        {
            dtLeaderBoard.WriteXml(leaderboardConfigFile);
        }

        private string GetGloatText(Bot winner, Bot loser, VictoryType victoryType, Options options)
        {            
            switch (victoryType)
            {
                case VictoryType.OutOfBounds:
                    return GetOutOfBoundsGloat(winner, loser, options);
                case VictoryType.ReducedToZeroHealth:
                    return GetZeroHeathGloat(winner, loser, options);
                case VictoryType.GivenOnDamage:
                    return $"{winner.Name} inflicted more damage";
                case VictoryType.GivenOnProgress:
                    return $"{winner.Name} made more progress";
                case VictoryType.Disconnect:
                    return $"{loser.Name} disconnected";
                default:
                    return null;
            }
        }

        private static string GetZeroHeathGloat(Bot winner, Bot loser, Options options)
        {
            var rand = new Random();
            var all = new List<string>()
            {
                $"{loser.Name} needs a screwdriver",
                $"{loser.Name} is spare parts",
                $"Does anyone have a spanner?",
                $"{loser.Name} is considering a career as a doorstop",
                "Knock Out"
            };

            if (options.IsChristmas)
            {
                all.AddRange(new string[]
                {
                    $"{winner.Name} made the naughty list",
                    $"Dancer and Prancer tried their best but only {winner.Name} got the prize",
                    $"{winner.Name} won -boosting his elf-esteem",
                    "Mistletoe!? more like missleToe!",
                    $"Christmas came early for {winner.Name}",
                    $"{winner.Name} reined down damage on that poor deer",
                    $"{loser.Name} should have asked for more flamethrower fuel for Christmas",
                    $"and like the turkey {loser.Name} is done"
            });
            }

            var index = rand.Next(0, all.Count - 1);
            return all.ElementAtOrDefault(index) ?? "Knock Out";
        }

        private string GetOutOfBoundsGloat(Bot winner, Bot loser, Options options)
        {
            var rand = new Random();
            var all = new List<string>()
            {
                $"{loser.Name} went for a swim",
                $"{loser.Name} took a dive",
                $"{loser.Name} doesn't like the water...",
                $"{loser.Name} makes a better anchor than a boat",
                $"{loser.Name} swam like a brick"
            };

            if (options.IsChristmas)
            {
                all.AddRange(new string[]
                {
                    $"{loser.Name} jingled all the way down",
                    $"{winner.Name}, {loser.Name} had snow-where else to go",
                    $"No sprout about it, the winner is {winner.Name}",
                 });
            }

            var index = rand.Next(0, all.Count - 1);
            return all.ElementAtOrDefault(index) ?? "Out of bounds!";
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
