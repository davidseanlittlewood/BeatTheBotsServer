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
    public class CheckForWinnerTests : CommonTestBase
    {
        [TestCase(3, 9, 100, 100, true, false, TestName = "Bot B drives backwards off the edge")]
        [TestCase(3, -1, 100, 100, true, false, TestName = "Bot B drives forwards off the edge")]
        [TestCase(9, 3, 100, 100, false, true, TestName = "Bot B drives backwards off the edge")]
        [TestCase(-1, 3, 100, 100, false, true, TestName = "Bot B drives forwards off the edge")]
        [TestCase(2, 3, 100, 100, false, false, TestName = "No Winner")]
        [TestCase(2, 3, 0, 100, false, true, TestName = "Bot A has critical damage")]
        [TestCase(2, 3, 100, 0, true, false, TestName = "Bot B has critical damage")]
        public void CheckVictory(int aPosition, int bPosition, int aHealth, int bHealth, bool aWinner, bool bWinner)
        {
            FirstBot.Position = aPosition;
            LastBot.Position = bPosition;
            FirstBot.Health = aHealth;
            LastBot.Health = bHealth;

            var winner = VictoryHelper.CheckForWinner(this.Arena, FirstBot, LastBot);

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
