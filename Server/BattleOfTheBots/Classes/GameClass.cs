using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.HTTP;
using System.Windows.Forms;
using BattleOfTheBots.Logic;
using BattleOfTheBots.State;


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

        private readonly Bot _bot1;
        private readonly Bot _bot2;

        public delegate void UpdateMatchProgressDelegate(GameClass currentGame, int gameCount, int totalGames);
        public event UpdateMatchProgressDelegate UpdateCurrentMatch;
          
        public string Winner { get { return _winner; }}
        public string Bot1Name { get { return this._bot1.Name; } }
        public string Bot2Name { get { return this._bot2.Name; } }

        public int Bot1Points { get { return this._bot1.Health; } }
        public int Bot2Points { get { return this._bot2.Health; } }

        public GameClass(Bot bot1, Bot bot2, int health, int flips, int flipOdds, int fuel, int arenaSize)
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

            if (HTTPUtility.SendStartInstruction(this._bot1, this._bot2, this._health, this._arenaSize, this._flips, this._flipOdds, this._fuel, this._bot1.DesiredDirection.ToString()[0]) == "failed")
            {
                AbandonBattle(this._bot2); 
                return;
            }

            if (HTTPUtility.SendStartInstruction(this._bot2, this._bot1, this._health, this._arenaSize, this._flips, this._flipOdds, this._fuel, this._bot2.DesiredDirection.ToString()[0]) == "failed")
            {
                AbandonBattle(this._bot1); 
                return;
            }

            IMoveManager moveManager = new MoveManager();
            var arena = new Arena(new Bot[] { this._bot1, this._bot2 });
      
            while (arena.Winner == null)
            {
                var botMove1 = new BotMove(this._bot1, HTTPUtility.GetMove(_bot1));
                var botMove2 = new BotMove(this._bot2, HTTPUtility.GetMove(_bot2));

                HTTPUtility.PostMove(this._bot1, botMove1.Move.ToString());
                HTTPUtility.PostMove(this._bot2, botMove2.Move.ToString());                
            }

            
            this._winner = arena.Winner.Name;

            }

        private void AbandonBattle(Bot winningBot)
        {
            this._winner = winningBot.Name;            
        }

    }
}
