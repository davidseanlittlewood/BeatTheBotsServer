using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Logic
{
    public class PositionHelpers
    {
        public static bool AreSideBySide(BotMove botA, BotMove botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Bot.Position, botB.Bot.Position);
            var rightee = Math.Max(botA.Bot.Position, botB.Bot.Position);


            return leftee + 1 == rightee;
        }

        public static bool AreSeperatedByOneSpace(BotMove botA, BotMove botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Bot.Position, botB.Bot.Position);
            var rightee = Math.Max(botA.Bot.Position, botB.Bot.Position);

            return leftee + 2 == rightee;
        }

        public static bool AreSeperatedByMoreThanOneSpace(BotMove botA, BotMove botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Bot.Position, botB.Bot.Position);
            var rightee = Math.Max(botA.Bot.Position, botB.Bot.Position);

            return rightee - leftee > 2;
        }
    }
}
