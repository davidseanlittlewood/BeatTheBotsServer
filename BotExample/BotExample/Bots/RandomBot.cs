using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample.Bots
{
    public class RandomBot : BotBaseClass
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

        public RandomBot()
        {
            this.PreviousMoves = new List<Move>();
        }

        /// <summary>
        /// Tell the server what we'd like to do in our move
        /// </summary>
        public override Move GetMove()
        {
            return GetRandomResponse(); // This bot isn't very smart - it'll pick a random move and carry it out
        }

        public override void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {
            OpponentName = opponentName;
            Health = health;
            ArenaSize = arenaSize;
            Flips = flips;
            FlipOdds = flipOdds;
            Fuel = fuel;
            Direction = direction;

            base.SetStartValues(opponentName, health, arenaSize, flips, flipOdds, fuel, direction);
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
                        return Move.Invalid; // flame throwers are not implemented on the server
                    }
                default:
                    {
                        if (Flips > 0) // if we have remaining flips
                        {
                            Flips--; // record that we're using a flip
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
