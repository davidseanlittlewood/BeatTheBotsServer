using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.AI
{
    /// <summary>
    /// Dolly is a clone she'll copy whatever you just did
    /// </summary>
    public class Dolly : Bot
    {
        public string OpponentName { get; set; }
        public List<Move> PreviousMoves { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }

        public Dolly(Direction direction)
            : base(direction, "Dolly")
        {
            this.PreviousMoves = new List<Move>();
        }

        public override BotMove GetMove()
        {
            Move move;
            if (this.IsFlipped)
            {
                move = Move.Flip;
            }
            else
            {

                move = this.PreviousMoves.LastOrDefault();
                if (move == null
                    || (move == Move.Flip && NumberOfFlipsRemaining == 0)
                    || (move == Move.FlameThrower && FlameThrowerFuelRemaining == 0))
                {
                    move = Move.Shunt;
                }
            }

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
    }
}
