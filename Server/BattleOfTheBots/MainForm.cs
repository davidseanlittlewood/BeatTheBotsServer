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
using System.Threading;

namespace BattleOfTheBots
{
    public partial class MainForm : Form
    {
        
        private Thread t;
        private LeaderboardForm leaderboard = new LeaderboardForm();       


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadBotConfig();
            LoadGameConfig();
        }


        private void gridGameConfig_Leave(object sender, EventArgs e)
        {
            gridGameConfig.EndEdit();
            SaveGameConfig();
        }

        private void gridBotConfig_Leave(object sender, EventArgs e)
        {
            gridBotConfig.EndEdit();
            SaveBotConfig();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveGameConfig();
            SaveBotConfig();
        }

        private void LoadBotConfig()
        {
            dsBotConfig.Clear();
            dsBotConfig.ReadXml("botConfiguration.xml");
        }

        private void SaveBotConfig()
        {
            dsBotConfig.WriteXml("botConfiguration.xml");
        }

        private void LoadGameConfig()
        {
            dsGameConfig.Clear();
            dsGameConfig.ReadXml("gameConfiguration.xml");
        }

        private void SaveGameConfig()
        {
            dsGameConfig.WriteXml("gameConfiguration.xml");
        }

        private delegate void OutputTextDelegate(string text);
        private void OutputText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new OutputTextDelegate(OutputText), (object)text);
            }
            else
                tbOutput.AppendText(text);
        }

        private delegate void UpdateCurrentMatchDelegate(GameClass game, int gameCount, int totalGames);
        private void UpdateCurrentMatch(GameClass game, int gameCount, int totalGames)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurrentMatchDelegate(UpdateCurrentMatch), (object)game, (object)gameCount, (object)totalGames);
            }
            else
            {
                leaderboard.UpdateCurrentMatch(string.Format("Game {4}/{5}:  {0} {2} vs {3} {1}", game.Bot1Name, game.Bot2Name, game.Bot1Points, game.Bot2Points, gameCount, totalGames));

                lblBot1Name.Text = game.Bot1Name;
                lblBot2Name.Text = game.Bot2Name;

                lblBot1Status.Text = string.Format("Game {0} in progress.", gameCount);
                lblBot2Status.Text = string.Format("Game {0} in progress.", gameCount);


                lblBot1Message.Text = game.Bot1Points.ToString();
                lblBot2Message.Text = game.Bot2Points.ToString();
            }
        }



        private void TestBots()
        {
            foreach (DataRow datarow in dtBotConfig.Rows)
            {
                BotClass bot = new BotClass(0, datarow[0].ToString(), datarow[1].ToString());
                OutputText(String.Format(">Testing {0} .......", bot.Name));

                if (HTTPUtility.GetMove(bot) != "failed")
                {

                    OutputText("test passed.\n");
                    datarow[3] = "Ok";
                }
                else
                {
                    datarow[2] = false;
                    datarow[3] = "Error";
                    OutputText(String.Format("{0} test failed:  Exception {1} \n", bot.Name, "failed to receive"));
                }
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestBots();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;

            if (t == null)
            {
                t = new Thread(new ThreadStart(this.RunGames));
                t.Start();
            }
            else
                t.Resume();
            
            

            startToolStripMenuItem.Enabled = false;
        }


        private void RunGames()
        {
            //start games
            List<GameClass> gamesList = CreateGamesList();
            

            int gameCount = 1;

            foreach (GameClass game in gamesList)
            {

                game.CommenceBattle(new GameClass.UpdateMatchProgressDelegate(UpdateCurrentMatch), gameCount, gamesList.Count);


                OutputText(string.Format(">Game {0}:  Winner {1}  {2}[{4}] vs {3}[{5}] \n", gameCount, game.Winner, game.Bot1Name, game.Bot2Name, game.Bot1Points, game.Bot2Points));
                leaderboard.RegisterBotWin(game.Winner);

                Thread.Sleep(1000);

                gameCount++;
                
            }
        }

        private List<GameClass> CreateGamesList()
        {
            List<GameClass> gamesList = new List<GameClass>();
            
            foreach (DataRow gameRow in dtGameConfig.Rows)
            {

                var enabledBots = from p in dtBotConfig.AsEnumerable()
                                  where p.Field<Boolean>("Enabled") == true
                                  select new
                                  {
                                      Name = p.Field<string>("Name"),
                                      Url = p.Field<string>("Url")
                                  };


                for (int x = 0; x < enabledBots.Count(); x++)
                {
                    for (int y = x + 1; y < enabledBots.Count(); y++)
                    {
                        var vBot1 = enabledBots.ElementAt(x);
                        var vBot2 = enabledBots.ElementAt(y);

                        gamesList.Add(
                            new GameClass(
                                new BotClass((int)gameRow["Dynamite"], vBot1.Name, vBot1.Url),
                                new BotClass((int)gameRow["Dynamite"], vBot2.Name, vBot2.Url),
                                (int)gameRow["MaxRounds"], (int)gameRow["TotalPoints"], (int)gameRow["Dynamite"]));
                    }


                }
            }
            return gamesList;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leaderboard.LoadConfiguration();
            leaderboard.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leaderboard.Hide();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leaderboard.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null)
            {
                t.Abort();
                t = null;
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Enabled = false;
            t.Suspend();
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.Abort();
            t = null;

            pauseToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;

            startToolStripMenuItem.Enabled = true;
        }
}
}
