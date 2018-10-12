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
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, Move.MoveForwards), new BotMove(this.LastBot, Move.MoveForwards));

            Assert.AreEqual(3, this.FirstBot.Position);
            Assert.AreEqual(4, this.LastBot.Position);
        }

        public MoveManager MoveManager { get; }
        public Arena Arena { get; set; }

        public Bot FirstBot => this.Arena?.Bots?.FirstOrDefault();
        public Bot LastBot => this.Arena?.Bots?.LastOrDefault();


        [SetUp]
        public void RestoreArena()
        {
            this.Arena = new Arena(new Bot[] { new Bot { Name = Guid.NewGuid().ToString(), Position = 3 }, new Bot { Name = Guid.NewGuid().ToString(), Position = 4 } });
        }
    }
}
