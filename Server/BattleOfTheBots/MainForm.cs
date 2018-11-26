using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BattleOfTheBots.Classes;
using BattleOfTheBots.HTTP;
using System.Threading;
using BattleOfTheBots.Logic;
using BattleOfTheBots.State;

namespace BattleOfTheBots
{
    public partial class MainForm : Form
    {
        
        private Thread t;
        private LeaderboardForm leaderboard = new LeaderboardForm();

        public bool IsChristmas { get; private set; }

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
            try
            {
                dsBotConfig.ReadXml("botConfiguration.xml");
            }
            catch { }
        }

        private void SaveBotConfig()
        {
            dsBotConfig.WriteXml("botConfiguration.xml");
        }

        private void LoadGameConfig()
        {            
            this.isChristmasToolStripMenuItem.Checked = DateTime.Now.AddDays(20) > new DateTime(DateTime.Now.Year, 12, 25);
            dsGameConfig.Clear();
            try
            {
                dsGameConfig.ReadXml("gameConfiguration.xml");
            }
            catch { }
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

        private delegate void UpdateCurrentMatchDelegate(Arena arena, GameClass game, int gameCount, int totalGames, BotMove botA, BotMove botB, Options options);
        private void UpdateCurrentMatch(Arena arena, GameClass game, int gameCount, int totalGames, BotMove botA, BotMove botB, Options options)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurrentMatchDelegate(UpdateCurrentMatch), arena, game, gameCount, totalGames, botA, botB, options);
            }
            else
            {
                var text = string.Format("Game {4}/{5}:  {0} {2} vs {3} {1}", game.Bot1.Name, game.Bot2.Name, game.Bot1.Health, game.Bot2.Health, gameCount, totalGames);
                leaderboard.UpdateCurrentMatch(arena,
                    text,
                    botA,
                    botB,
                    options);

                lblBot1Name.Text = game.Bot1.Name;
                lblBot2Name.Text = game.Bot2.Name;

                lblBot1Status.Text = string.Format("Game {0} in progress.", gameCount);
                lblBot2Status.Text = string.Format("Game {0} in progress.", gameCount);


                lblBot1Message.Text = string.Format("Health {0} Flips remaining {1} Flame thrower fuel {2}", game.Bot1.Health, game.Bot1.NumberOfFlipsRemaining, game.Bot1.FlameThrowerFuelRemaining);
                lblBot2Message.Text = string.Format("Health {0} Flips remaining {1} Flame thrower fuel {2}", game.Bot2.Health, game.Bot2.NumberOfFlipsRemaining, game.Bot2.FlameThrowerFuelRemaining);
            }
        }



        private void TestBots()
        {
            foreach (DataRow datarow in dtBotConfig.Rows)
            {
                Bot bot = GetClassForBot(Direction.Left, datarow["Url"].ToString(), datarow["Name"].ToString());
                OutputText(String.Format(">Testing {0} .......", bot.Name));

                if (bot.GetMove().Move != Logic.Move.Invalid)
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
                int retries = 0;
                bool success = false;
                do
                {
                    try
                    {
                        leaderboard?.SaveLeaderboard();
                        RunGame(gamesList.Count(), gameCount, game);
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message} {ex.StackTrace}");
                        retries++;
                    }
                } while (!success && retries < 3);

                gameCount++;

            }
        }

        private void RunGame(int allGameCount, int currentGameCount, GameClass game)
        {
            var options = new Options
            {
                IsChristmas = this.isChristmasToolStripMenuItem.Checked
            };
            var arena = game.CommenceBattle(UpdateCurrentMatch, currentGameCount, allGameCount, options);

            if (!string.IsNullOrWhiteSpace(game.Winner))
            {
                OutputText(string.Format(">Game {0}:  Winner {1}  \n", currentGameCount, game.Winner));

                var winner = arena.Winner;
                var loser = arena.Bots.Except(new Bot[] { winner }).Single();
                leaderboard.RegisterBotWin(winner, loser, arena.VictoryType ?? VictoryType.None, options);
            }
            else
            {
                leaderboard.RegisterDraw();
                OutputText(string.Format(">Game {0}:  Draw\n", currentGameCount));
            }

            Thread.Sleep(2500);
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

                        var bot1Class = GetClassForBot(Direction.Right, vBot1.Url, vBot1.Name);
                        var bot2Class = GetClassForBot(Direction.Left, vBot2.Url, vBot2.Name);
                        bot1Class.AddCompetitor(bot2Class);
                        bot2Class.AddCompetitor(bot1Class);

                        var bothBots = new Bot[] { bot1Class, bot2Class }.OrderBy(b => Guid.NewGuid()).ToArray();
                        bothBots.First().DesiredDirection = Direction.Right;
                        bothBots.Last().DesiredDirection = Direction.Left;

                        gamesList.Add(
                            new GameClass(
                                bothBots.First(),
                                bothBots.Last(),
                                Convert.ToInt16(gameRow["Health"]), Convert.ToInt16(gameRow["Flips"]), Convert.ToInt16(gameRow["FlipOdds"]), Convert.ToInt16(gameRow["Fuel"]),
                                Convert.ToInt16(gameRow["ArenaSize"])));
                    }
                }
            }
            return gamesList.OrderBy(g => Guid.NewGuid()).ToList(); // randomise the game order
        }

        private Bot GetClassForBot(Direction direction, string url, string name)
        {
            switch (url)
            {
                case "AI.Random":
                    return new AI.RandomBot(direction);

                case "AI.Lemming":
                    return new AI.LemmingBot(direction);

                case "AI.Dolly":
                    return new AI.Dolly(direction);

                case "AI.Spock":
                    return new AI.SpockBot(direction);

                case "AI.Bash":
                    return new AI.BashBot(direction);

                case "AI.Cogs":
                    return new AI.Cogs(direction);

                case "AI.Cassandra":
                    return new AI.LadyCassandra(direction);

                default:
                    return new RemoteBot(direction, url, name);
            }
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

        private void randomBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Random Bot", "AI.Random", true, "OK");
        }

        private void lemmingBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Lemming Bot", "AI.Lemming", true, "OK");
        }

        private void simpleBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Sir Spock", "AI.Spock", true, "OK");
        }

        private void bashBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Mr Bash", "AI.Bash", true, "OK");
        }

        private void cogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Cogs", "AI.Cogs", true, "OK");
        }

        private void ladyCassandraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Lady Cassandra", "AI.Cassandra", true, "OK");
        }

        private void dollyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dtBotConfig.Rows.Add("Dolly", "AI.Dolly", true, "OK");
        }
    }
}
