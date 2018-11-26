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
        public static Bot CheckForWinner(Arena arena, Bot botA, Bot botB, out VictoryType victoryType)
        {
            Bot winner = null;
            victoryType = VictoryType.None;
            if (IsBotOutOfBounds(arena, botA)) winner = botB;
            if (IsBotOutOfBounds(arena, botB)) winner = botA;

            if(winner != null)
            {
                victoryType = VictoryType.OutOfBounds;
            }

            if (botA.Health <= 0 && botB.Health <= 0) // did we both run out of health at the same time?
            {                
                winner = GetBotWhoMadeMostProgress(arena, botA, botB);
                victoryType = winner == null
                    ? VictoryType.Draw
                    : VictoryType.GivenOnProgress;
            }
            else if (botA.Health <= 0)
            {
                victoryType = VictoryType.ReducedToZeroHealth;
                winner = botB;
            }
            else if (botB.Health <= 0)
            {
                victoryType = VictoryType.ReducedToZeroHealth;
                winner = botA;
            }

            if(botA.IsFlipped && botB.IsFlipped && botA.NumberOfFlipsRemaining == 0 && botB.NumberOfFlipsRemaining == 0)
            {               
                winner = GetBotWhoIsAhead(arena, botA, botB, out victoryType);
                if(victoryType == VictoryType.None) // if we can't decide a winner but we're both stuck then it's a draw
                {
                    victoryType = VictoryType.Draw;
                }
            }

            return winner;
        }

        public static Bot GetBotWhoIsAhead(Arena arena, Bot botA, Bot botB, out VictoryType victoryType)
        {
            Bot winner = null;
            if (botA.Health != botB.Health)
            {
                victoryType = VictoryType.GivenOnDamage;
                winner = GetBothWithMostHealth(botA, botB);
            }
            else
            {                
                winner = GetBotWhoMadeMostProgress(arena, botA, botB);
                if(winner != null)
                {
                    victoryType = VictoryType.GivenOnProgress;
                }
                else
                {
                    victoryType = VictoryType.None;
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
