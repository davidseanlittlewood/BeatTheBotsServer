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
    public class AdvancingWithdrawingStatusChecks : CommonTestBase
    {
        [TestCase(false, Move.MoveForwards, true)]
        [TestCase(false, Move.Shunt, true)]
        [TestCase(true, Move.MoveForwards, false)]
        [TestCase(true, Move.Shunt, false)]
        [TestCase(false, Move.Invalid, false)]
        [TestCase(false, Move.AttackWithAxe, false)]
        [TestCase(false, Move.Flip, false)]
        [TestCase(false, Move.MoveBackwards, false)]
        public void IsBotAdvancing(bool flipped, Move move, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.IsBotAdvancing(new BotMove(new Bot(Direction.Left) { IsFlipped = flipped }, move)));
        }

        [TestCase(false, Move.MoveBackwards, true)]
        [TestCase(true, Move.MoveBackwards, false)]
        [TestCase(false, Move.Invalid, false)]
        [TestCase(false, Move.AttackWithAxe, false)]
        [TestCase(false, Move.Flip, false)]
        [TestCase(false, Move.MoveForwards, false)]
        [TestCase(false, Move.Shunt, false)]
        public void IsBotWithdrawing(bool flipped, Move move, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.IsBotWithdrawing(new BotMove(new Bot(Direction.Left) { IsFlipped = flipped }, move)));
        }

        [TestCase(false, Move.MoveForwards, false, Move.MoveForwards, true)]
        [TestCase(false, Move.MoveForwards, false, Move.MoveBackwards, true)]
        [TestCase(false, Move.MoveForwards, true, Move.MoveForwards, true)]
        [TestCase(true, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveBackwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.AttackWithAxe, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Flip, true)]
        [TestCase(true, Move.Shunt, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Shunt, true)]
        [TestCase(true, Move.Shunt, true, Move.Shunt, false)]
        public void IsEitherAdvancing(bool aFlipped, Move aMove, bool bFlipped, Move bMove, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.IsEitherAdvancing(
                new BotMove(new Bot(Direction.Right) { IsFlipped = aFlipped }, aMove),
                new BotMove(new Bot(Direction.Left) { IsFlipped = bFlipped }, bMove)
                ));
        }

        [TestCase(false, Move.MoveForwards, false, Move.MoveForwards, true)]
        [TestCase(false, Move.MoveForwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(true, Move.MoveForwards, true, Move.MoveForwards, false)]
        [TestCase(false, Move.MoveBackwards, false, Move.MoveBackwards, false)]
        [TestCase(false, Move.AttackWithAxe, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Flip, false)]
        [TestCase(true, Move.Shunt, false, Move.Flip, false)]
        [TestCase(false, Move.Shunt, false, Move.Shunt, true)]
        [TestCase(true, Move.Shunt, true, Move.Shunt, false)]
        public void AreBothAdvancing(bool aFlipped, Move aMove, bool bFlipped, Move bMove, bool expected)
        {
            Assert.AreEqual(expected, this.MoveManager.AreBothAdvancing(
                new BotMove(new Bot(Direction.Right) { IsFlipped = aFlipped }, aMove),
                new BotMove(new Bot(Direction.Left) { IsFlipped = bFlipped }, bMove)
                ));
        }
    }
}
