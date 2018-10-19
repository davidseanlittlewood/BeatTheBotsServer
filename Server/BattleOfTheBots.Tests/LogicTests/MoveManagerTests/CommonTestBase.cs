using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using NUnit.Framework;
using System.Linq;

namespace BattleOfTheBots.Tests.LogicTests.MoveManagerTests
{
    public abstract class CommonTestBase
    {
        protected CommonTestBase() => this.MoveManager = new MoveManager();

        public MoveManager MoveManager { get; }

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