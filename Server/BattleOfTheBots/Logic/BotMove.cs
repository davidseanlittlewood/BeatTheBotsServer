using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Logic
{
    public class BotMove
    {
        public BotMove(Bot bot, Move move)
        {
            Bot = bot;
            Move = move;
        }

        public Bot Bot { get; }
        public Move Move { get; }
    }
}
