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
    public class OneBotShuntsAnotherTests
    {
        public OneBotShuntsAnotherTests() => this.MoveManager = new MoveManager();

        public MoveManager MoveManager { get; }

        [TestCase(3, 4, Move.Shunt, Move.MoveForwards, 4, 5, 95, 100, TestName = "A shunts B while both side by side")]
        [TestCase(3, 4, Move.MoveForwards, Move.Shunt, 2, 3, 100, 95, TestName = "B shunts A while both side by side")]
        public void ShuntCheck(int firstPosition, int lastPosition, Move firstMove, Move lastMove, int firstExpPosition, int lastExpPosition, int firstExpHealth, int lastExpHealth)
        {
            this.FirstBot.Position = firstPosition;
            this.LastBot.Position = lastPosition;
            this.MoveManager.OneBotShuntsAnother(this.Arena, new BotMove(this.FirstBot, firstMove), new BotMove(this.LastBot, lastMove));

            Assert.AreEqual(firstExpPosition, this.FirstBot.Position);
            Assert.AreEqual(lastExpPosition, this.LastBot.Position);

            Assert.AreEqual(firstExpHealth, this.FirstBot.Health);
            Assert.AreEqual(lastExpHealth, this.LastBot.Health);
        }

        [TestCase(3, 4, Move.Shunt, Move.Shunt, 4, 5, 95, 100, TestName = "Will error if two shunts")]
        [TestCase(3, 4, Move.MoveForwards, Move.MoveForwards, 4, 5, 95, 100, TestName = "Will error if no shunts")]
        [TestCase(3, 7, Move.MoveForwards, Move.Shunt, 2, 3, 100, 95, TestName = "Will error if not in contact")]        
        public void ErrorShuntCheck(int firstPosition, int lastPosition, Move firstMove, Move lastMove, int firstExpPosition, int lastExpPosition, int firstExpHealth, int lastExpHealth)
        {
            this.FirstBot.Position = firstPosition;
            this.LastBot.Position = lastPosition;

            Assert.Throws<InvalidOperationException>(() => this.MoveManager.OneBotShuntsAnother(this.Arena, new BotMove(this.FirstBot, firstMove), new BotMove(this.LastBot, lastMove)));
        }



        public Arena Arena { get; set; }

        public Bot FirstBot => this.Arena?.Bots?.FirstOrDefault();
        public Bot LastBot => this.Arena?.Bots?.LastOrDefault();


        [SetUp]
        public void RestoreArena()
        {
            this.Arena = new Arena(new Bot[]
            {
                new Bot(Direction.Right) { Name = "Left Bot", Position = 3, Health = 100, NumberOfFlipsRemaining = 5 },
                new Bot(Direction.Left) { Name = "Right Bot", Position = 4, Health = 100, NumberOfFlipsRemaining = 5 }
            });
        }
    }
}
