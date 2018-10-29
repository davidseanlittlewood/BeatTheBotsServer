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
            bot1.Position = CalculateStartPositionForBot(arenaSize, bot1.DesiredDirection);

            bot2.Health = health;
            bot2.NumberOfFlipsRemaining = flips;
            bot2.FlameThrowerFuelRemaining = fuel;
            bot2.Position = CalculateStartPositionForBot(arenaSize, bot2.DesiredDirection);

            this._arenaSize = arenaSize;
            this._flipOdds = flipOdds;

            this._bot1 = bot1;
            this._bot2 = bot2;

        }

        public static int CalculateStartPositionForBot(int arenaSize, Direction direction)
        {
            return direction == Direction.Right
                ? (int)Math.Floor(arenaSize / 2D) - 1
                : (int)Math.Ceiling(arenaSize / 2D);
        }

        public void CommenceBattle(UpdateMatchProgressDelegate updateCurrentMatch, int gameCount, int totalGames)
        {

            updateCurrentMatch += this.UpdateCurrentMatch;

            if (this._bot1.SendStartInstruction(this._bot2.Name, this._arenaSize, this._flipOdds) == "failed")
            {
                AbandonBattle(this._bot2); 
                return;
            }

            if (this._bot1.SendStartInstruction(this._bot1.Name, this._arenaSize, this._flipOdds) == "failed")
            {
                AbandonBattle(this._bot1);
                return;
            }

            IMoveManager moveManager = new MoveManager();
            var arena = new Arena(new Bot[] { this._bot1, this._bot2 });

            Console.WriteLine($"Starting game between {_bot1.Name} and {_bot2.Name} in an arena with {arena.NumberOfSquares} spaces");

            while (arena.Winner == null)
            {
                var botMove1 = this._bot1.GetMove();
                var botMove2 = this._bot2.GetMove();

                Console.WriteLine($"{botMove1.Bot.Name} move is {botMove1.Move}/{botMove2.Bot.Name} move is {botMove2.Move}");

                moveManager.ProcessMove(arena, botMove1, botMove2);

                LogBotStatus(botMove1);
                LogBotStatus(botMove2);                

                this._bot1.PostOpponentsMove(botMove2.Move);
                this._bot2.PostOpponentsMove(botMove1.Move);

                updateCurrentMatch(this, gameCount, totalGames);
            }


            Console.WriteLine($"The winner was {arena.Winner.Name}!");
            this._winner = arena.Winner.Name;

            }

        private void LogBotStatus(BotMove botMove)
        {
            Console.WriteLine($"{botMove.Bot.Name} - Health: {botMove.Bot.Health} Position: {botMove.Bot.Position} Fuel: {botMove.Bot.FlameThrowerFuelRemaining} Flips: {botMove.Bot.NumberOfFlipsRemaining} IsFlipped: {botMove.Bot.IsFlipped}");
        }

        private void AbandonBattle(Bot winningBot)
        {
            this._winner = winningBot.Name;            
        }

    }
}
