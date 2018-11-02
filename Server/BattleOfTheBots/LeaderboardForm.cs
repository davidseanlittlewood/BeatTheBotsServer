using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using BattleOfTheBots.UIControl;
using BattleOfTheBots.Logic;
using System.Threading;

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

        public delegate void UpdateCurrentMatchdelegate(string matchDetails, BotMove botA, BotMove botB);

        public void UpdateCurrentMatch(string matchDetails, BotMove leftBot, BotMove rightBot)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurrentMatchdelegate(UpdateCurrentMatch), matchDetails, leftBot, rightBot);
            }
            else
            {                            
                for (int i = 1; i <= 3; i++)
                {

                    lblCurrentMatch.Text = matchDetails;
                    lblCurrentMatch.Refresh();

                    botUI.Update(9, leftBot, rightBot, i);
                    
                    Thread.Sleep(150); // this isn't good - it blocks the UI thread making it unresponsive
                }
            }
        }


        public delegate void RegisterBotWinDelegate(string botname);
 
        public void RegisterBotWin(string botname)
        {

            if (InvokeRequired)
            {
                Invoke(new RegisterBotWinDelegate(RegisterBotWin), (object)botname);
            }
            else
            {
                DataRow botRow;
                DataRow[] rows = dtLeaderBoard.Select(string.Format("Name = '{0}'", botname));
                if (rows.Length > 0)
                {
                    botRow = rows.First();
                    botRow["Wins"] = (Int32)botRow["Wins"] + 1;
                }
                else
                {
                    botRow = dtLeaderBoard.NewRow();
                    botRow["Name"] = botname;
                    botRow["Wins"] = 1;

                    dtLeaderBoard.Rows.Add(botRow);
                }

                SortResults();
                gridLeaderboard.Refresh();

                dtLeaderBoard.WriteXml(leaderboardConfigFile);
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
