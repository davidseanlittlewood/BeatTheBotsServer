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

                if(winner == null) // if they've both made the same amount of progress then just give it to the guy on the left, how's that for fair? ;-)
                {
                    winner = botA;
                }
            }

            return winner;
        }

        public static Bot GetBotWhoMadeMostProgress(Arena arena, params Bot[] bots)
        {
            var leftBot = bots.Single(b => b.DesiredDirection == Direction.Right); // the left bot is the one which wants to go right
            var leftProgress = leftBot.Position;

            var rightBot = bots.Single(b => b.DesiredDirection == Direction.Left); // the right bot is the one which wants to go left
            var rightProgress = (arena.NumberOfSquares - 1 - rightBot.Position);

            if(leftProgress == rightProgress)
            {
                return null;
            }

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
