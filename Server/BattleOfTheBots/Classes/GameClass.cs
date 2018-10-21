using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.HTTP;
using System.Windows.Forms;


namespace BattleOfTheBots.Classes
{
    public class GameClass
    {
        private readonly int _health;
        private readonly int _flips;
        private readonly int _flipOdds;
        private readonly int _fuel;
        private readonly int _arenaSize;
        private string _winner = string.Empty;

        private readonly BotClass _bot1;
        private readonly BotClass _bot2;

        public delegate void UpdateMatchProgressDelegate(GameClass currentGame, int gameCount, int totalGames);
        public event UpdateMatchProgressDelegate UpdateCurrentMatch;
          
        public string Winner { get { return _winner; }}
        public string Bot1Name { get { return this._bot1.Name; } }
        public string Bot2Name { get { return this._bot2.Name; } }

        public int Bot1Points { get { return this._bot1.Health; } }
        public int Bot2Points { get { return this._bot2.Health; } }

        public GameClass(BotClass bot1, BotClass bot2, int health, int flips, int flipOdds, int fuel, int arenaSize)
        {
            this._health = health;
            this._flips = flips;
            this._flipOdds = flipOdds;
            this._fuel = fuel;
            this._arenaSize = arenaSize;
            
            this._bot1 = bot1;
            this._bot2 = bot2;

        }

        public void CommenceBattle(UpdateMatchProgressDelegate updateCurrentMatch, int gameCount, int totalGames)
        {

            updateCurrentMatch += this.UpdateCurrentMatch;

            if (HTTPUtility.SendStartInstruction(this._bot1, this._bot2, this._health, this._arenaSize, this._flips, this._flipOdds, this._fuel, 'R') == "failed")
            {
                AbandonBattle(this._bot2); 
                return;
            }

            if (HTTPUtility.SendStartInstruction(this._bot2, this._bot1, this._health, this._arenaSize, this._flips, this._flipOdds, this._fuel, 'L') == "failed")
            {
                AbandonBattle(this._bot1); 
                return;
            }


            int points = 1;
            bool matchIsOver = false;

            while (!matchIsOver)
            {
                this._bot1.LastMove = HTTPUtility.GetMove(this._bot1);
                this._bot2.LastMove = HTTPUtility.GetMove(this._bot2);

                HTTPUtility.PostMove(this._bot1, this._bot2.LastMove);
                HTTPUtility.PostMove(this._bot2,this._bot1.LastMove);

                if (this._bot1.LastMove == "failed")
                {
                    AbandonBattle(this._bot2);
                    return;
                }
                
                if (this._bot2.LastMove == "failed")
                {
                    AbandonBattle(this._bot1); 
                    return;
                }

                switch (WinningMove(this._bot1.LastMove, this._bot2.LastMove))
                {
                    case 0:
                    {
                        points++;
                        break;
                    }
                    case 1:
                        { 
                        this._bot2.Health = this._bot2.Health - points;
                        points = 1;
                        break;
                        }
                    case 2:
                    {
                        this._bot1.Health = this._bot2.Health - points;
                        points = 1;
                        break;
                    }
                    default:
                        break;
                }
                matchIsOver = ((this._bot1.Health < 0) ||
                                    (this._bot2.Health < 0));

                updateCurrentMatch(this, gameCount, totalGames);
            }

            RegisterBattleWinner();            
        }

        private void AbandonBattle(BotClass winningBot)
        {
            this._winner = winningBot.Name;            

            RegisterBattleWinner();
        }

        private void RegisterBattleWinner()
        {
            if (this._bot1.Health > this._bot2.Health)
            {
                this._winner = this._bot1.Name;
            }
            else if (this._bot2.Health > this._bot1.Health)
            {
                this._winner = this._bot2.Name;
            }
            else
            {
                this._winner = "tie";
            }

        }


        private int WinningMove(string move1, string move2)
        {
          return 0;
        }

    }
}
