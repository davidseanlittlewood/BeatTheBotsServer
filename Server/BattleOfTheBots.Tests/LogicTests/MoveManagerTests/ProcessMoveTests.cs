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
        [TestCase(Move.AttackWithAxe, Move.MoveForwards, 3, 4, 100, 90, TestName = "A attacks with an axe while B moves forwards")]
        [TestCase(Move.MoveForwards, Move.AttackWithAxe, 3, 4, 90, 100, TestName = "A moves forward while B attacks with an axe")]
        [TestCase(Move.AttackWithAxe, Move.AttackWithAxe, 3, 4, 90, 90, TestName = "Both attack with axes")]
        public void CheckMove(Move botAMove, Move botBMove, int botAExpectedPosition, int botBExpectedPosition, int botAExpectedHealth = 100, int botBExpectedHealth = 100)
        {
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botAMove), new BotMove(this.LastBot, botBMove));

            Assert.AreEqual(botAExpectedPosition, this.FirstBot.Position, "Bot A was in an incorrect position");
            Assert.AreEqual(botBExpectedPosition, this.LastBot.Position, "Bot B was in an incorrect position");

            Assert.AreEqual(botAExpectedHealth, this.FirstBot.Health, "Bot A has an incorrect health");
            Assert.AreEqual(botBExpectedHealth, this.LastBot.Health, "Bot B has an incorrect health");
        }

        public MoveManager MoveManager { get; }
        public Arena Arena { get; set; }

        public Bot FirstBot => this.Arena?.Bots?.FirstOrDefault();
        public Bot LastBot => this.Arena?.Bots?.LastOrDefault();


        [SetUp]
        public void RestoreArena()
        {
            this.Arena = new Arena(new Bot[] 
            {
                new Bot { Name = Guid.NewGuid().ToString(), Position = 3, Health = 100 },
                new Bot { Name = Guid.NewGuid().ToString(), Position = 4, Health = 100 }
            });            
        }
    }
}
