using BattleOfTheBots.Logic;
using NUnit.Framework;
using System;

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

        }

        public MoveManager MoveManager { get; }
    }
}
