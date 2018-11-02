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
          
        public string Winner { get { return _winner; }}

        public Bot Bot1 { get { return this._bot1; } }
        public Bot Bot2 { get { return this._bot2; } }

        public GameClass(Bot bot1, Bot bot2, int health, int flips, int flipOdds, int fuel, int arenaSize)
        {
            bot1.StartingHealth = health;
            bot1.Health = health;
            bot1.StartingNumberOfFlips = flips;
            bot1.NumberOfFlipsRemaining = flips;
            bot1.StartingFlameThrowerFuel = fuel;
            bot1.FlameThrowerFuelRemaining = fuel;
            bot1.Position = CalculateStartPositionForBot(arenaSize, bot1.DesiredDirection);

            bot2.StartingHealth = health;
            bot2.Health = health;
            bot2.StartingNumberOfFlips = flips;
            bot2.NumberOfFlipsRemaining = flips;
            bot2.StartingFlameThrowerFuel = fuel;
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

        public void CommenceBattle(Action<GameClass, int , int,BotMove, BotMove> updateAction, int gameCount, int totalGames)
        {
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
            var arena = new Arena(new Bot[] { this._bot1, this._bot2 }, _arenaSize);

            Console.WriteLine($"Starting game between {_bot1.Name} (starting at position {_bot1.Position}) and {_bot2.Name} (at position {_bot2.Position}) in an arena with {arena.NumberOfSquares} spaces");

            int totalHealth;
            int? lastTotalHealth;
            int unchangedHealthSpins = 0;
            totalHealth = _bot1.Health + _bot2.Health;

            while (arena.Winner == null)
            {
                var botMove1 = this._bot1.GetMove();
                var botMove2 = this._bot2.GetMove();

                Console.WriteLine($"{botMove1.Bot.Name} move is {botMove1.Move}/{botMove2.Bot.Name} move is {botMove2.Move}");

                moveManager.ProcessMove(arena, botMove1, botMove2);

                lastTotalHealth = totalHealth;
                totalHealth = _bot1.Health + _bot2.Health;
                if (totalHealth == lastTotalHealth)
                {
                    unchangedHealthSpins++;
                }
                else
                {
                    unchangedHealthSpins = 0;
                }

                LogBotStatus(botMove1);
                LogBotStatus(botMove2);

                this._bot1.PostOpponentsMove(botMove2.Move);
                this._bot2.PostOpponentsMove(botMove1.Move);

                updateAction(this, gameCount, totalGames, botMove1, botMove2);


                // This match is getting boring, let's find the bot who is ahead and declare them as the winner
                if (unchangedHealthSpins > arena.NumberOfTurnsWithNoDamageToTolerate)
                {                                        
                    arena.Winner = VictoryHelper.GetBotWhoIsAhead(arena, _bot1, _bot2);
                    if (arena.Winner != null)
                    {
                        Console.WriteLine($"The game aborted after {unchangedHealthSpins} moves with no damage, {arena.Winner.Name} was given the victory because they were ahead");
                    }
                    else
                    {
                        Console.WriteLine($"The game aborted after {unchangedHealthSpins} moves with no damage, no bot could be identified as the clear winner");
                    }
                    break;
                }
            }



            if (arena.Winner != null)
            {
                Console.WriteLine($"The winner was {arena.Winner.Name}!");
                var loser = new Bot[] { _bot1, _bot2 }.Except(new Bot[] { arena.Winner }).Single();
                Console.WriteLine($"The loser was {loser.Name}");
                this._winner = arena.Winner.Name;
            }
            else
            {
                Console.WriteLine($"There was no winner...");
            }
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
