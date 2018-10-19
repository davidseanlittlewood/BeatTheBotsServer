using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Tests.LogicTests.MoveManagerTests
{
    public class ShuntingStatusChecks : CommonTestBase
    {
        [TestCase(false, Move.MoveForwards, false, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveForwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(true, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveBackwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.AttackWithAxe, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Flip, true)]
        [TestCase(true, Move.Shunt, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Shunt, true)]
        [TestCase(true, Move.Shunt, true, Move.Shunt, false)]
        public void IsEitherShunting(bool aFlipped, Move aMove, bool bFlipped, Move bMove, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.EitherBotIsShunting(
                new BotMove(new Bot(Direction.Right) { IsFlipped = aFlipped }, aMove),
                new BotMove(new Bot(Direction.Left) { IsFlipped = bFlipped }, bMove)
                ));
        }

        [TestCase(false, Move.MoveForwards, false, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveForwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(true, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveBackwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.AttackWithAxe, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Flip, false)]
        [TestCase(true, Move.Shunt, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Shunt, true)]
        [TestCase(true, Move.Shunt, true, Move.Shunt, false)]
        public void AreBothShunting(bool aFlipped, Move aMove, bool bFlipped, Move bMove, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.BothBotsAreShunting(
                new BotMove(new Bot(Direction.Right) { IsFlipped = aFlipped }, aMove),
                new BotMove(new Bot(Direction.Left) { IsFlipped = bFlipped }, bMove)
                ));
        }
    }
}
