using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;



namespace BotExample
{
    public abstract class BotBaseClass
    {
        public string OpponentName { get; set; }
        public string LastOpponentsMove { get; set; }
        public bool Flipped { get; set; }
        public int Health { get; set; }
        public int Flips { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }
        public char Direction { get; set; }

        /* Method called when start instruction is received
         *
         * POST http://<your_bot_url>/start
         *
         */
        public void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {
            OpponentName = opponentName;
            Health = health;
            ArenaSize = arenaSize;
            Flips= flips;
            FlipOdds = flipOdds;
            Fuel = fuel;
            Direction = direction;
        }

        /* Method called when move instruction is received instructing opponents move
         *
         * POST http://<your_bot_url>/move
         *
         */ 
        public void SetLastOpponentsMove(string lastOpponentsMove)
        {
            LastOpponentsMove = lastOpponentsMove;
        }

        /* Method called when status instruction is received instructing 
         *
         * POST http://<your_bot_url>/status
         *
         */
        public void SetFlippedStatus(bool flipped)
        {
            Flipped = flipped;
        }

        /* Method called when move instruction is received requesting your move
         *
         * GET http://<your_bot_url>/move
         *
         */
        public virtual string GetMove()
        {
            return "HIT";            
        }   
    }
}
