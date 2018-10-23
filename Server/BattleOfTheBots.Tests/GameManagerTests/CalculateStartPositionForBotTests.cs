using BattleOfTheBots.Classes;
using BattleOfTheBots.State;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Tests.GameManagerTests
{
    public class CalculateStartPositionForBotTests
    {
        [TestCase(9, Direction.Right, 3)]
        [TestCase(9, Direction.Left, 5)]
        [TestCase(3, Direction.Right, 0)]
        [TestCase(3, Direction.Left, 2)]
        [TestCase(2, Direction.Right, 0)]
        [TestCase(2, Direction.Left, 1)]
        [TestCase(6, Direction.Right, 2)]
        [TestCase(6, Direction.Left, 3)]
        public void VerifyStartPosition(int arenaSize, Direction direction, int expectedPosition)
        {
            var result = GameClass.CalculateStartPositionForBot(arenaSize, direction);

            Assert.AreEqual(expectedPosition, result);
        }
    }
}
