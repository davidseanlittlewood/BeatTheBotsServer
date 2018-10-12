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

        [TestCase(Move.MoveForwards, Move.MoveForwards, 3, 4, TestName = "When both Bots move forward then neither will move")]
        [TestCase(Move.MoveBackwards, Move.MoveBackwards, 2, 5, TestName = "When both bots move backwards then both will move back a space")]
        [TestCase(Move.MoveForwards, Move.MoveBackwards, 4, 5, TestName = "When Bot A moves forwards and B moves backwards then both will move to the right")]
        [TestCase(Move.MoveBackwards, Move.MoveForwards, 2, 3, TestName = "When Bot A moves backwards and B moves forwards then both will move to the left")]
        [TestCase(Move.AttackWithAxe, Move.MoveForwards, 3, 4, 100, 90, TestName = "When Bot A attacks with an axe while B moves forwards then there will be no impact on A but B will remain still and take damage")]
        [TestCase(Move.MoveForwards, Move.AttackWithAxe, 3, 4, 90, 100, TestName = "When Bot A moves forward while B attacks with an axe then there will be no impact on B but A will remain still and take damage")]
        [TestCase(Move.AttackWithAxe, Move.AttackWithAxe, 3, 4, 90, 90, TestName = "When both Bots attack with axes then both will take damage")]
        [TestCase(Move.Shunt, Move.MoveForwards, 4, 5, 95, 100, TestName = "When Bot A shunts and Bot B moves forward then Bot A will take damage and move right and Bot B will be pushed right")]
        [TestCase(Move.MoveForwards, Move.Shunt, 2, 3, 100, 95, TestName = "When Bot A moves forward and Bot B shunts then Bot A will be pushed left, Bot B will take damage and move left")]
        [TestCase(Move.Shunt, Move.MoveBackwards, 4, 5, 100, 100, TestName = "When Bot A shunts and Bot B moves backwards then both bots will move to the right and no damage is taken")]
        [TestCase(Move.MoveBackwards, Move.Shunt, 2, 3, 100, 100, TestName = "When Bot A moves backwards and Bot B shunts then both will move to the left but no damage is taken")]
        [TestCase(Move.Shunt, Move.Shunt, 3, 4, 95, 95, TestName = "When both Bots shunt then neither will move but both will take damage")]
        [TestCase(Move.Flip, Move.Flip, 3, 4, 100, 100, true, true, TestName = "When both bots flip then both bots will be turned upside down")]
        [TestCase(Move.Flip, Move.Shunt, 3, 5, 100, 95, false, true, TestName = "When bot A flips and bot B shunts then bot B will be damanged, turned upside down, and moved right a space")]
        [TestCase(Move.Flip, Move.AttackWithAxe, 3, 4, 90, 100, false, true, TestName = "When bot A flips and bot B attacks with an axe then bot A will be damaged but bot B will be flipped")]
        [TestCase(Move.Flip, Move.MoveForwards, 3, 4, 100, 100, false, true, TestName = "When bot A flips and bot B moves forward then bot A will be unaffected but bot B will be flipped")]
        [TestCase(Move.Flip, Move.MoveBackwards, 3, 5, 100, 100, false, false, TestName = "When bot A flips and bot B moves backwards then B will move to the right")]
        [TestCase(Move.Shunt, Move.Flip, 2, 5, 95, 100, true, false, TestName = "When bot A shunts and bot B flips then B will be unaffected but A will be damaged, flipped, and moved to the left")]
        [TestCase(Move.AttackWithAxe, Move.Flip, 3, 4, 100, 90, true, false, TestName = "When bot A attacks with an axe and bot B flips then A will be flipped but B will be damaged")]
        [TestCase(Move.MoveForwards, Move.Flip, 3, 4, 100, 100, true, false, TestName = "When bot A moves forward and bot B flips then bot B will be turned over")]
        [TestCase(Move.MoveBackwards, Move.Flip, 2, 4, 100, 100, false, false, TestName ="When bot A moves backward and bot B flips then bot A will move left")]
        public void CheckMove(Move botAMove, Move botBMove, int botAExpectedPosition, int botBExpectedPosition, int botAExpectedHealth = 100, int botBExpectedHealth = 100, bool expectedFlipAStatus = false, bool expectedFlipBStatus = false)
        {
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botAMove), new BotMove(this.LastBot, botBMove));

            Assert.AreEqual(botAExpectedPosition, this.FirstBot.Position, "Bot A was in an incorrect position");
            Assert.AreEqual(botBExpectedPosition, this.LastBot.Position, "Bot B was in an incorrect position");

            Assert.AreEqual(botAExpectedHealth, this.FirstBot.Health, "Bot A has an incorrect health");
            Assert.AreEqual(botBExpectedHealth, this.LastBot.Health, "Bot B has an incorrect health");

            Assert.AreEqual(expectedFlipAStatus, this.FirstBot.IsFlipped, "Bot A is incorrectly flipped");
            Assert.AreEqual(expectedFlipBStatus, this.LastBot.IsFlipped, "Bot B is incorrectly flipped");
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
                new Bot { Name = Guid.NewGuid().ToString(), Position = 3, Health = 100, NumberOfFlipsRemaining = 5 },
                new Bot { Name = Guid.NewGuid().ToString(), Position = 4, Health = 100, NumberOfFlipsRemaining = 5 }
            });            
        }
    }
}
