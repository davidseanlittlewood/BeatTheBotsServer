using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.State;

namespace BattleOfTheBots.Logic
{
    public class VictoryHelper
    {
        public static Bot CheckForWinner(Arena arena, Bot botA, Bot botB)
        {
            Bot winner = null;
            if (IsBotOutOfBounds(arena, botA)) winner = botB;
            if (IsBotOutOfBounds(arena, botB)) winner = botA;

            if (botA.Health <= 0) winner = botB;
            if (botB.Health <= 0) winner = botA;

            return winner;
        }

        public static bool IsBotOutOfBounds(Arena arena, Bot bot)
        {
            if (bot.Position < 0) return true;
            if (bot.Position > arena.NumberOfSquares - 1) return true;
            return false;
        }
    }
}
