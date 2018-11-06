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
    /// <summary>
    /// All Bot implementations should inherit from this class
    /// </summary>
    public abstract class BotBaseClass
    {

        /// <summary>
        /// Override this method to capture any game start information you wish
        /// 
        /// Method called when start instruction is received
        /// POST http://<your_bot_url>/start
        /// </summary>
        /// <param name="opponentName">The name of your opponent</param>
        /// <param name="health">How much health your bot has</param>
        /// <param name="arenaSize">How many squares the arena long the arena is</param>
        /// <param name="flips">How many flips you have available</param>
        /// <param name="flipOdds">The probability of making a successful flip</param>
        /// <param name="fuel">How much fuel you have for your flame thrower</param>
        /// <param name="direction">The direction in which you are facing</param>
        /// <paramref name="startPosition">The location index where your bot is starting</param>
        public virtual void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction, int startPosition)
        {
        }

        /// <summary>
        /// Override to capture any information about your opponent's last move
        /// 
        /// Method called when move instruction is received instructing opponents move
        /// POST http://<your_bot_url>/move
        /// </summary>
        /// <param name="lastOpponentsMove">The last move your opponent made</param>        
        public virtual void CaptureOpponentsLastMove(Move lastOpponentsMove)
        {
        }

        /// <summary>
        /// An update sent to your bot to alert you if you have been flipped onto your back
        /// 
        /// Method called when status instruction is received instructing          
        /// 
        /// POST http://<your_bot_url>/status
        /// </summary>
        /// <param name="flipped">A bool indicating whether or not you have been flipped</param>
        public virtual void SetFlippedStatus(bool flipped)
        {            
        }

        public virtual void SetOpponentFlippedStatus(bool flipped)
        {
        }

        /// <summary>
        /// The method which is called to request your move, if not overridden your BOT will always attack with an axe
        /// 
        /// 
        /// Method called when move instruction is received requesting your move
        /// 
        /// GET http://<your_bot_url>/move
        /// </summary>
        public virtual Move GetMove()
        {
            return Move.AttackWithAxe;
        }   
    }
}
