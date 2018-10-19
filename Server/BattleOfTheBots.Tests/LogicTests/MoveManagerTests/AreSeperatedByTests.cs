﻿using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Tests.LogicTests.MoveManagerTests
{
    public class AreSeperatedByTests : CommonTestBase
    {
        [Test]
        public void AreSideBySideTests()
        {
            var botARanges = Enumerable.Range(0, this.Arena.NumberOfSquares - 2); // minus the count/space and minus a space for the other bot
            foreach(var botAPosition in botARanges)
            {
                var botBPosition = botAPosition + 1;

                FirstBot.Position = botAPosition;
                LastBot.Position = botBPosition;

                Assert.IsTrue(this.MoveManager.AreSideBySide(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)), $"When BotA is at {botAPosition} and BotB is at {botBPosition} they are not reported as consecutive spaces");

                var botBRange = Enumerable.Range(botBPosition + 1, this.Arena.NumberOfSquares - botBPosition - 1);
                foreach(var botBNotInContactPosition in botBRange)
                {                    
                    LastBot.Position = botBNotInContactPosition;

                    Assert.IsFalse(this.MoveManager.AreSideBySide(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)), $"When BotA is at {botAPosition} and BotB is at {botBNotInContactPosition} they are reported as consecutive spaces");
                }
            }
        }

        [Test]
        public void AreSeperatedByOneSpaceTests()
        {
            var botARanges = Enumerable.Range(0, this.Arena.NumberOfSquares - 2); // minus the count/space and minus a space for the other bot
            foreach (var botAPosition in botARanges)
            {
                var botBPosition = botAPosition + 2;

                FirstBot.Position = botAPosition;
                LastBot.Position = botBPosition;

                Assert.IsTrue(this.MoveManager.AreSeperatedByOneSpace(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)),
                    $"When Bot A is at {botAPosition} and Bot B is at {botBPosition} they should show as seperated by a single space");

                var botBRange = Enumerable.Range(botBPosition + 1, this.Arena.NumberOfSquares - botBPosition - 1);
                foreach (var botBNotInContactPosition in botBRange)
                {
                    LastBot.Position = botBNotInContactPosition;
                    Assert.IsFalse(this.MoveManager.AreSeperatedByOneSpace(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)),
                    $"When Bot A is at {botAPosition} and Bot B is at {botBPosition} they should not show as seperated by a single space");
                }
            }
        }

        [TestCase(3, 5, true)]
        [TestCase(5, 3, true)]
        [TestCase(3, 6, false)]
        [TestCase(2, 7, false)]
        public void AreSeperatedByOneSpaceInTheWrongOrder(int positionA, int positionB, bool expectedResult)
        {
            FirstBot.Position = positionA;
            LastBot.Position = positionB;

            Assert.AreEqual(expectedResult, this.MoveManager.AreSeperatedByOneSpace(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)));
        }

        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(2, 0, false)]
        [TestCase(0, 3, true)]
        [TestCase(0, 4, true)]
        [TestCase(4, 0, true)]        
        public void AreSeperatedByMoreThanOneSpaceTests(int botA, int botB, bool expectedResult)
        {
            FirstBot.Position = botA;
            LastBot.Position = botB;


            Assert.AreEqual(expectedResult, this.MoveManager.AreSeperatedByMoreThanOneSpace(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)),
                    $"When Bot A is at {botA} and Bot B is at {botB} the expected result was {expectedResult}");

        }


        [Test]
        public void AreSeperatedByMoreThanOneSpaceTests()
        {
            var botARanges = Enumerable.Range(0, this.Arena.NumberOfSquares - 2); // minus the count/space and minus a space for the other bot
            foreach (var botAPosition in botARanges)
            {
                var minBotBPosition = botAPosition + 3;
                var maxBotBPosition = this.Arena.NumberOfSquares - 1;
                if(minBotBPosition > maxBotBPosition)
                {
                    continue;
                }
                var numberOfSpacesBehindBotB = maxBotBPosition - minBotBPosition + 1;

                var botBRange = Enumerable.Range(minBotBPosition, numberOfSpacesBehindBotB);
                foreach (var botBNotInContactPosition in botBRange)
                {
                    FirstBot.Position = botAPosition;
                    LastBot.Position = botBNotInContactPosition;

                    Assert.IsTrue(this.MoveManager.AreSeperatedByMoreThanOneSpace(new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, Move.AttackWithAxe)),
                    $"When Bot A is at {botAPosition} and Bot B is at {minBotBPosition} they should show as seperated by more than a single space");
                }
            }
        }
    }
}
