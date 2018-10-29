using BattleOfTheBots.Logic;
using BattleOfTheBots.Tests.LogicTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Tests.VictoryHelperTests
{
    public class GetBotWhoMadeMostProgressTests : CommonTestBase
    {
        [TestCase(3, 5, 9, false, false)]
        [TestCase(4, 5, 9, true, false)]
        [TestCase(3, 4, 9, false, true)]
        [TestCase(1, 2, 5, false, true)]
        [TestCase(3, 4, 5, true, false)]
        public void GetBotWhoMadeMostProgress(int aPosition, int bPosition, int arenaSize, bool aWinner, bool bWinner)
        {
            FirstBot.Position = aPosition;
            LastBot.Position = bPosition;
            Arena.NumberOfSquares = arenaSize;

            var winner = VictoryHelper.GetBotWhoMadeMostProgress(this.Arena, FirstBot, LastBot);

            if(aWinner)
            {
                Assert.AreEqual(FirstBot, winner, "Bot A should have won");
            }
            else if (bWinner)
            {
                Assert.AreEqual(LastBot, winner, "Bot B should have won");
            }
            else
            {
                Assert.IsNull(winner, "No winner should have been declared");
            }
        }

    }
}
