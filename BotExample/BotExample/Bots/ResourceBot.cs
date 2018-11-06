using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample.Bots
{
    public class ResourceBot : BotBaseClass
    {
        public string OpponentName { get; set; }
        public List<Move> PreviousMoves { get; set; }
        public bool Flipped { get; set; }
        public int Health { get; set; }
        public int Flips { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }
        public char Direction { get; set; }

        public ResourceBot()
        {
            this.PreviousMoves = new List<Move>();
        }

        /// <summary>
        /// Tell the server what we'd like to do in our move
        /// </summary>
        public override Move GetMove()
        {
            return GetResponse(); // This bot isn't very smart - it'll pick a random move and carry it out
        }

        public override void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction, int startIndex)
        {
            OpponentName = opponentName;
            Health = health;
            ArenaSize = arenaSize;
            Flips = flips;
            FlipOdds = flipOdds;
            Fuel = fuel;
            Direction = direction;

            base.SetStartValues(opponentName, health, arenaSize, flips, flipOdds, fuel, direction, startIndex);
        }

        public override void CaptureOpponentsLastMove(Move lastOpponentsMove)
        {
            PreviousMoves.Add(lastOpponentsMove);

            base.CaptureOpponentsLastMove(lastOpponentsMove);
        }

        public override void SetFlippedStatus(bool flipped)
        {
            Flipped = flipped;

            base.SetFlippedStatus(flipped);
        }

        /// <summary>
        /// Select a random response
        /// </summary>
        /// <returns></returns>
        public Move GetResponse()
        {

            if (this.Flipped && Flips > 0)
            {
                Flips--;
                Flipped = false;
                return Move.Flip;
            }



            if (this.Fuel > 0)
                return Move.FlameThrower;
            
            return Move.Shunt;
        }
    }
}
