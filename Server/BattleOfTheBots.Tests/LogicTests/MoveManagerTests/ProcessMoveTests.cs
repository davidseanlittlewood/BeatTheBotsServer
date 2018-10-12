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

        [TestCase(Move.MoveForwards, Move.MoveForwards, 3, 4, TestName = "Both move forwards")]
        [TestCase(Move.MoveBackwards, Move.MoveBackwards, 2, 5, TestName = "Both move backwards")]
        [TestCase(Move.MoveForwards, Move.MoveBackwards, 4, 5, TestName = "A moves forwards, B moves backwards")]
        [TestCase(Move.MoveBackwards, Move.MoveForwards, 2, 3, TestName = "A moves backwards, B moves forwards")]
        public void CheckMove(Move botAMove, Move botBMove, int botAExpectedPosition, int botBExpectedPosition)
        {
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botAMove), new BotMove(this.LastBot, botBMove));

            Assert.AreEqual(botAExpectedPosition, this.FirstBot.Position);
            Assert.AreEqual(botBExpectedPosition, this.LastBot.Position);
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
