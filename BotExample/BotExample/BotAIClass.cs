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
    internal static class BotAIClass
    {
        private static string _opponentName;
        private static string _lastOpponentsMove;
        private static bool _flipped = false;
        private static int _health;
        private static int _flips;
        private static int _fuel;
        private static int _flipOdds;
        private static int _arenaSize;
        private static char _direction;

        /* Method called when start instruction is received
         *
         * POST http://<your_bot_url>/start
         *
         */
        internal static void SetStartValues(string opponentName, int health, int arenaSize, int flips, int flipOdds, int fuel, char direction)
        {
            _opponentName = opponentName;
            _health = health;
            _arenaSize = arenaSize;
            _flips = flips;
            _flipOdds = flipOdds;
            _fuel = fuel;
            _direction = direction;
        }

        /* Method called when move instruction is received instructing opponents move
         *
         * POST http://<your_bot_url>/move
         *
         */ 
        public static void SetLastOpponentsMove(string lastOpponentsMove)
        {           
            _lastOpponentsMove = lastOpponentsMove;
        }

        /* Method called when status instruction is received instructing 
         *
         * POST http://<your_bot_url>/status
         *
         */
        public static void SetFlippedStatus(bool flipped)
        {
            _flipped = flipped;
        }

        /* Method called when move instruction is received requesting your move
         *
         * GET http://<your_bot_url>/move
         *
         */
        internal static string GetMove()
        {
            return GetRandomResponse();
        }

        internal static string GetRandomResponse()
        {
           
            Random random = new System.Random(Environment.TickCount);            
            int rnd = random.Next(6);
            switch (rnd)
            {
                case 0:
                    {
                        return "FORWARD";
                    }
                case 1:
                    {
                        return "BACK";
                    }
                case 2:
                    {
                        return "HIT";
                    }
                case 3:
                {
                    return "SHUNT";
                }
                case 4:
                    {
                        _fuel--;
                        return "FLAME";
                    }
                default:
                    {
                        _flips--;
                        return "FLIP";
                    }
            }
        }       
    }
}
