using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.AI
{
    public class RandomBot : Bot
    {
        public string OpponentName { get; set; }
        public List<Move> PreviousMoves { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }

        public RandomBot(Direction direction)
            : base(direction, "RandomBot")
        {
            this.PreviousMoves = new List<Move>();
        }

        public override BotMove GetMove()
        {
            var move = GetRandomResponse(); // This bot isn't very smart - it'll pick a random move and carry it out
            return new BotMove(this, move);
        }

        public override void PostOpponentsMove(Move move)
        {
            PreviousMoves.Add(move);
        }

        public override string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            OpponentName = opponentBotName;
            ArenaSize = arenaSize;
            FlipOdds = flipOdds;

            return "OK";
        }


        /// <summary>
        /// Select a random response
        /// </summary>
        /// <returns></returns>
        public Move GetRandomResponse()
        {
            Random random = new System.Random(Environment.TickCount);
            int rnd = random.Next(6);
            switch (rnd)
            {
                case 0:
                    {
                        return Move.MoveForwards;
                    }
                case 1:
                    {
                        return Move.MoveBackwards;
                    }
                case 2:
                    {
                        return Move.AttackWithAxe;
                    }
                case 3:
                    {
                        return Move.Shunt;
                    }
                case 4:
                    {
                        Fuel--;
                        return Move.FlameThrower;
                    }
                default:
                    {
                        if (this.NumberOfFlipsRemaining > 0) // if we have remaining flips
                        {
                            return Move.Flip; // and try to turn them over
                        }
                        else // otherwise just hit them with an axe
                        {
                            return Move.AttackWithAxe;
                        }
                    }
            }
        }
    }
}
