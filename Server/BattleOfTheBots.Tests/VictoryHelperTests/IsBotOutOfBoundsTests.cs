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
    public class IsBotOutOfBoundsTests : CommonTestBase
    {
        [TestCase(9, -1, true)]
        [TestCase(9, 0, false)]
        [TestCase(9, 1, false)]
        [TestCase(9, 7, false)]
        [TestCase(9, 8, false)]
        [TestCase(9, 9, true)]
        [TestCase(9, 10, true)]
        [TestCase(3, -1, true)]
        [TestCase(3, 2, false)]
        [TestCase(3, 3, true)]
        [TestCase(5, -1, true)]
        [TestCase(5, 2, false)]
        [TestCase(5, 5, true)]
        public void IsFirstBotOutOfBounts(int arenaSize, int firstBotLocation, bool expected)
        {
            Arena.NumberOfSquares = arenaSize;
            this.FirstBot.Position = firstBotLocation;

            Assert.AreEqual(expected, VictoryHelper.IsBotOutOfBounds(this.Arena, this.FirstBot));
        }

        [TestCase(9, -1, true)]
        [TestCase(9, 0, false)]
        [TestCase(9, 1, false)]
        [TestCase(9, 7, false)]
        [TestCase(9, 8, false)]
        [TestCase(9, 9, true)]
        [TestCase(9, 10, true)]
        [TestCase(3, -1, true)]
        [TestCase(3, 2, false)]
        [TestCase(3, 3, true)]
        [TestCase(5, -1, true)]
        [TestCase(5, 2, false)]
        [TestCase(5, 5, true)]
        public void IsSecondBotOutOfBounts(int arenaSize, int secondBotLocation, bool expected)
        {
            Arena.NumberOfSquares = arenaSize;
            this.LastBot.Position = secondBotLocation;

            Assert.AreEqual(expected, VictoryHelper.IsBotOutOfBounds(this.Arena, this.LastBot));
        }
    }
}
