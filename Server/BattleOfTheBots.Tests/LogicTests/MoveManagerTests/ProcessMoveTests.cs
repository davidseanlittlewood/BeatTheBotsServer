using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using NUnit.Framework;
using System;
using System.Linq;

namespace BattleOfTheBots.Tests.LogicTests.MoveManagerTests
{
    public class ProcessMoveTests
    {
        public ProcessMoveTests()
        {
            this.MoveManager = new MoveManager();
        }

        [Test]
        public void CheckMove()
        {
            var arena = new Arena(new Bot[] { new Bot { Name = Guid.NewGuid().ToString(), Position = 3 }, new Bot { Name = Guid.NewGuid().ToString(), Position = 4 } });
            this.MoveManager.ProcessMove(arena, new BotMove(arena.Bots.First(), Move.MoveForwards), new BotMove(arena.Bots.Last(), Move.MoveForwards));

            Assert.AreEqual(3, arena.Bots.First().Position);
            Assert.AreEqual(4, arena.Bots.Last().Position);
        }

        public MoveManager MoveManager { get; }
    }
}
