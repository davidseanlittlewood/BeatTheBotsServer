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

        private readonly int _flipOdds;
        private readonly int _arenaSize;

        private string _winner = string.Empty;

        private readonly Bot _bot1;
        private readonly Bot _bot2;

        public delegate void UpdateMatchProgressDelegate(GameClass currentGame, int gameCount, int totalGames);
        public event UpdateMatchProgressDelegate UpdateCurrentMatch;
          
        public string Winner { get { return _winner; }}

        public Bot Bot1 { get { return this._bot1; } }
        public Bot Bot2 { get { return this._bot2; } }

        public GameClass(Bot bot1, Bot bot2, int health, int flips, int flipOdds, int fuel, int arenaSize)
        {
            bot1.Health = health;
            bot1.NumberOfFlipsRemaining = flips;
            bot1.FlameThrowerFuelRemaining = fuel;
            bot1.Position = 2;

            bot2.Health = health;
            bot2.NumberOfFlipsRemaining = flips;
            bot2.FlameThrowerFuelRemaining = fuel;
            bot2.Position = 2;
       
            this._arenaSize = arenaSize;
            this._flipOdds = flipOdds;

            this._bot1 = bot1;
            this._bot2 = bot2;

        }

        public void CommenceBattle(UpdateMatchProgressDelegate updateCurrentMatch, int gameCount, int totalGames)
        {

            updateCurrentMatch += this.UpdateCurrentMatch;

            if (HTTPUtility.SendStartInstruction(this._bot1, this._bot2, this._bot1.Health, this._arenaSize, this._bot1.NumberOfFlipsRemaining, this._flipOdds, this._bot1.FlameThrowerFuelRemaining, this._bot1.DesiredDirection.ToString()[0]) == "failed")
            {
                AbandonBattle(this._bot2); 
                return;
            }

            if (HTTPUtility.SendStartInstruction(this._bot2, this._bot1, this._bot2.Health, this._arenaSize, this.Bot2.NumberOfFlipsRemaining, this._flipOdds, this.Bot2.FlameThrowerFuelRemaining, this._bot2.DesiredDirection.ToString()[0]) == "failed")
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

                moveManager.ProcessMove(arena, botMove1, botMove2);

                HTTPUtility.PostMove(this._bot1, botMove1.Move.ToString());
                HTTPUtility.PostMove(this._bot2, botMove2.Move.ToString());

                updateCurrentMatch(this, gameCount, totalGames);


            }

            
            this._winner = arena.Winner.Name;

            }

        private void AbandonBattle(Bot winningBot)
        {
            this._winner = winningBot.Name;            
        }

    }
}
