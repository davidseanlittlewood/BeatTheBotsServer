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
        public static Bot CheckForWinner(Arena arena, BotMove botA, BotMove botB)
        {
            Bot winner = null;
            if (botA.Bot.Position < 0) winner = botB.Bot;
            if (botB.Bot.Position >= arena.NumberOfSquares) winner = botA.Bot;

            if (botA.Bot.Health <= 0) winner = botB.Bot;
            if (botB.Bot.Health <= 0) winner = botA.Bot;

            return winner;
        }
    }
}
