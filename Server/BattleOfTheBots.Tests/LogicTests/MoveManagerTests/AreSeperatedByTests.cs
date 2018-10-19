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
    public class AreSeperatedByTests
    {
        public AreSeperatedByTests()
        {
            this.MoveManager = new MoveManager();
        }

        [Test]
        public void AreSideBySideTests()
        {
            var botARanges = Enumerable.Range(0, this.Arena.NumberOfSquares - 2); // minus the count/space and minus a space for the other bot
            foreach(var botAPosition in botARanges)
            {
                var botBPosition = botAPosition + 1;

                FirstBot.Position = botAPosition;
                LastBot.Position = botBPosition;

                Assert.IsTrue(this.MoveManager.AreSideBySide(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)));
            }
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
                new Bot(Direction.Right) { Name = Guid.NewGuid().ToString(), Position = 3, Health = 100, NumberOfFlipsRemaining = 5 },
                new Bot(Direction.Left) { Name = Guid.NewGuid().ToString(), Position = 4, Health = 100, NumberOfFlipsRemaining = 5 }
            });
        }

    }
}
