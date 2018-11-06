using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample.Bots
{
    public class Flipper : BotBaseClass
    {
        public string OpponentName { get; set; }
        public List<Move> PreviousMoves { get; set; }
        public bool Flipped { get; set; }
        public bool OpponentFlipped { get; set; }
        public int Health { get; set; }
        public int Flips { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }
        public char Direction { get; set; }
        public bool firstmove = true;

        public Flipper()
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
            firstmove = true;
            Flipped = false;
            OpponentFlipped = false;

            base.SetStartValues(opponentName, health, arenaSize, flips, flipOdds, fuel, direction, startIndex);
        }

        public override void CaptureOpponentsLastMove(Move lastOpponentsMove)
        {
            if (lastOpponentsMove == Move.Flip && OpponentFlipped)
                OpponentFlipped = false;

            PreviousMoves.Add(lastOpponentsMove);

            base.CaptureOpponentsLastMove(lastOpponentsMove);
        }

        public override void SetFlippedStatus(bool flipped)
        {
            Flipped = flipped;

            base.SetFlippedStatus(flipped);
        }
        public override void SetOpponentFlippedStatus(bool opponentFlipped)
        {
            Flips--;
            OpponentFlipped = opponentFlipped;            
            base.SetOpponentFlippedStatus(opponentFlipped);
        }

        /// <summary>
        /// Select a random response
        /// </summary>
        /// <returns></returns>
        public Move GetResponse()
        {
            if (firstmove)
            {
                firstmove = false;
                return (Move.MoveForwards);                
            }

            if (Flips > 0  && !OpponentFlipped)
            {
                if (Flipped)
                    Flips--;

                Flipped = false;
                return Move.Flip;
            }
            else
                return Move.Shunt;
        }
    }
}
