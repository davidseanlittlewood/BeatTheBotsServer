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

            if(botA.IsFlipped && botB.IsFlipped && botA.NumberOfFlipsRemaining == 0 && botB.NumberOfFlipsRemaining == 0)
            {
                if(botA.Health != botB.Health)
                {
                    winner = GetBothWithMostHealth(botA, botB);
                }
                else
                {
                    winner = GetBotWhoMadeMostProgress(arena, botA, botB);
                }
            }

            return winner;
        }

        public static Bot GetBotWhoMadeMostProgress(Arena arena, params Bot[] bots)
        {
            var leftBot = bots.Single(b => b.DesiredDirection == Direction.Left);
            var leftProgress = leftBot.Position;

            var rightBot = bots.Single(b => b.DesiredDirection == Direction.Right);
            var rightProgress = (arena.NumberOfSquares - 1 - rightBot.Position);

            var maxProgressBot = leftProgress > rightProgress // in even width arenas this will favour one bot (assuming they're both flipped and have equal amounts of damage)
                ? leftBot
                : rightBot;

            return maxProgressBot;
        }

        public static Bot GetBothWithMostHealth(params Bot[] bots)
        {
            return bots.OrderByDescending(b => b.Health).First();
        }

        public static bool IsBotOutOfBounds(Arena arena, Bot bot)
        {
            if (bot.Position < 0) return true;
            if (bot.Position > arena.NumberOfSquares - 1) return true;
            return false;
        }
    }
}
