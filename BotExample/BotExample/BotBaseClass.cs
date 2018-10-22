﻿using System;
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

        /* Method called when start instruction is received
         *
         * POST http://<your_bot_url>/start
         *
         */
        public virtual void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {
        }

        /* Method called when move instruction is received instructing opponents move
         *
         * POST http://<your_bot_url>/move
         *
         */ 
        public virtual void SetLastOpponentsMove(string lastOpponentsMove)
        {            
        }

        /* Method called when status instruction is received instructing 
         *
         * POST http://<your_bot_url>/status
         *
         */
        public virtual void SetFlippedStatus(bool flipped)
        {            
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
