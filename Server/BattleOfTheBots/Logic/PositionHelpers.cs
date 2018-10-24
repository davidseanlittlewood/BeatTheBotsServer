using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Logic
{
    public class PositionHelpers
    {
        public static bool AreSideBySide(Bot botA, Bot botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Position, botB.Position);
            var rightee = Math.Max(botA.Position, botB.Position);


            return leftee + 1 == rightee;
        }

        public static bool AreSeperatedByOneSpace(Bot botA, Bot botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Position, botB.Position);
            var rightee = Math.Max(botA.Position, botB.Position);

            return leftee + 2 == rightee;
        }

        public static bool AreSeperatedByMoreThanOneSpace(Bot botA, Bot botB)
        {
            // We want to make sure this works regardless of which order the bots are supplied so check min/max to unscramble the positions
            var leftee = Math.Min(botA.Position, botB.Position);
            var rightee = Math.Max(botA.Position, botB.Position);

            return rightee - leftee > 2;
        }

        public static bool IsWithinXOfEdge(int position, int arenaSize, int x)
        {
            if (position < x) return true;
            if (arenaSize - position - 1 < x) return true;
            return false;
        }
    }
}
