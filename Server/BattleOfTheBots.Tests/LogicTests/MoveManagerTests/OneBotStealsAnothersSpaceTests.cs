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
    public class OneBotStealsAnothersSpaceTests : CommonTestBase
    {
        [TestCase(0, 4, 5, TestName = "A steals the space from B")]
        [TestCase(1, 3, 4, TestName = "B steals the space from A")]
        public void StealFromTheLeftCheck(int hareIndex, int firstExpPosition, int lastExpPosition)
        {
            var hare = this.Arena.Bots.ElementAt(hareIndex);
            var tortoise = this.Arena.Bots.Except(new Bot[] { hare }).Single();
            
            this.MoveManager.OneBotStealsAnothersSpace(new BotMove(tortoise, Move.Invalid), new BotMove(hare, Move.Invalid));

            Assert.AreEqual(firstExpPosition, this.FirstBot.Position);
            Assert.AreEqual(lastExpPosition, this.LastBot.Position);

            Assert.AreEqual(100, this.FirstBot.Health);
            Assert.AreEqual(100, this.LastBot.Health);
        }

        [TestCase(3, 4, TestName = "Will error if Bots are face to face")]
        [TestCase(3, 6, TestName = "Will error bots have too big a space")]        
        public void ErrorStealCheck(int firstPosition, int lastPosition)
        {
            this.FirstBot.Position = firstPosition;
            this.LastBot.Position = lastPosition;

            Assert.Throws<InvalidOperationException>(() => this.MoveManager.OneBotStealsAnothersSpace(new BotMove(this.FirstBot, Move.Invalid), new BotMove(this.LastBot, Move.Invalid)));
        }

        public override void RestoreArena()
        {
            this.Arena = new Arena(new Bot[]
            {
                new Bot(Direction.Right) { Name = "Left Bot", Position = 3, Health = 100, NumberOfFlipsRemaining = 5 },
                new Bot(Direction.Left) { Name = "Right Bot", Position = 5, Health = 100, NumberOfFlipsRemaining = 5 }
            });
        }
    }
}
