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
        [TestCase(3, 9, 100, 100, true, false, VictoryType.OutOfBounds, TestName = "Bot B drives backwards off the edge")]
        [TestCase(3, -1, 100, 100, true, false, VictoryType.OutOfBounds, TestName = "Bot B drives forwards off the edge")]
        [TestCase(9, 3, 100, 100, false, true, VictoryType.OutOfBounds, TestName = "Bot B drives backwards off the edge")]
        [TestCase(-1, 3, 100, 100, false, true, VictoryType.OutOfBounds, TestName = "Bot B drives forwards off the edge")]
        [TestCase(2, 3, 100, 100, false, false, VictoryType.None, TestName = "No Winner")]
        [TestCase(2, 3, 0, 100, false, true, VictoryType.ReducedToZeroHealth, TestName = "Bot A has critical damage")]
        [TestCase(2, 3, 100, 0, true, false, VictoryType.ReducedToZeroHealth, TestName = "Bot B has critical damage")]
        [TestCase(3, 4, 0, -5, true, false, VictoryType.GivenOnDamage, TestName = "Bot A has no health Bot B has negative health but more progress then A should win")]
        [TestCase(4, 5, -5, 0, false, true, VictoryType.GivenOnDamage, TestName = "Bot A negative health but more progress and Bot B has no health then B should win")]
        [TestCase(4, 5, 0, 0, true, false, VictoryType.GivenOnProgress, TestName = "Both bots run out of health with bot A position advantage")]
        [TestCase(3, 4, 0, 0, false, true, VictoryType.GivenOnProgress, TestName = "Both bots run out of health with bot B position advantage")]
        [TestCase(3, 5, 0, 0, false, false, VictoryType.Draw, TestName = "Both bots run out of health with no position advantage")]
        [TestCase(3, 5, -5, -5, false, false, VictoryType.Draw, TestName = "Both bots negative health health with no position advantage")]
        public void CheckVictory(int aPosition, int bPosition, int aHealth, int bHealth, bool aWinner, bool bWinner, VictoryType expectedVictoryType)
        {
            FirstBot.Position = aPosition;
            LastBot.Position = bPosition;
            FirstBot.Health = aHealth;
            LastBot.Health = bHealth;

            VictoryType victoryType;
            var winner = VictoryHelper.CheckForWinner(this.Arena, FirstBot, LastBot, out victoryType);

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
            Assert.AreEqual(expectedVictoryType, victoryType, "The victory type was incorrect");
        }

        [Test]
        public void CheckNoWinnerWhenFlippedButFlipsRemain()
        {
            FirstBot.IsFlipped = true;
            LastBot.IsFlipped = true;
            FirstBot.NumberOfFlipsRemaining = 1;
            LastBot.NumberOfFlipsRemaining = 0;
            VictoryType victoryType;

            var winner = VictoryHelper.CheckForWinner(this.Arena, FirstBot, LastBot, out victoryType);

            Assert.IsNull(winner);
            Assert.AreEqual(VictoryType.None, victoryType);
        }

        [TestCase(2, 3, 100, 100, false, true, VictoryType.GivenOnProgress, TestName = "Bot B has made more progress and so should win")]
        [TestCase(6, 8, 100, 100, true, false, VictoryType.GivenOnProgress, TestName = "Bot A has made more progress and so should win")]
        [TestCase(2, 3, 100, 85, true, false, VictoryType.GivenOnDamage, TestName = "Bot B has made more progress but Bot A has more health so A should win")]
        [TestCase(6, 8, 85, 100, false, true, VictoryType.GivenOnDamage, TestName = "Bot A has made more progress but Bot B has more health so B should win")]
        [TestCase(2, 3, 85, 100, false, true, VictoryType.GivenOnDamage, TestName = "Bot B has made more progress and has more health so B should win")]
        [TestCase(6, 8, 100, 85, true, false, VictoryType.GivenOnDamage, TestName = "Bot A has made more progress and has more health so A should win")]
        [TestCase(3, 5, 85, 85, false, false, VictoryType.Draw, TestName = "Both bots have the same progress and health so neither bot should win")]
        public void CheckWhenBothFlipped(int aPosition, int bPosition, int aHealth, int bHealth, bool aWinner, bool bWinner, VictoryType expectedVictoryType)
        {
            FirstBot.Position = aPosition;
            LastBot.Position = bPosition;
            FirstBot.Health = aHealth;
            LastBot.Health = bHealth;
            FirstBot.IsFlipped = true;
            LastBot.IsFlipped = true;
            FirstBot.NumberOfFlipsRemaining = 0;
            LastBot.NumberOfFlipsRemaining = 0;

            VictoryType victoryType;
            var winner = VictoryHelper.CheckForWinner(this.Arena, FirstBot, LastBot, out victoryType);

            if (aWinner)
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
            Assert.AreEqual(expectedVictoryType, victoryType);
        }
    }
}
