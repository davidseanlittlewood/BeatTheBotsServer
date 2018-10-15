using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using BattleOfTheBots.Classes;
using BattleOfTheBots.HTTP;
using System.Linq;
using System.IO;


namespace BattleOfTheBots
{


    public partial class LeaderboardForm : Form
    {
        private string leaderboardConfigFile = "leaderboard.xml";

        public LeaderboardForm()
        {
            InitializeComponent();
           
        }

        public void Clear()
        {
            dtLeaderBoard.Rows.Clear();
            gridLeaderboard.DataSource = dtLeaderBoard;
            gridLeaderboard.Refresh();
        }

        public delegate void UpdateCurrentMatchdelegate(string matchDetails);

        public void UpdateCurrentMatch(string matchDetails)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurrentMatchdelegate(UpdateCurrentMatch), (object) matchDetails);
            }
            else
            {
                lblCurrentMatch.Text = matchDetails;
                lblCurrentMatch.Refresh();
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
