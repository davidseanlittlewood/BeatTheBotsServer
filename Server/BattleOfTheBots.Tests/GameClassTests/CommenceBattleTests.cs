using BattleOfTheBots.Classes;
using BattleOfTheBots.State;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Tests.GameClassTests
{
    public class CommenceBattleTests
    {
        [TestCase(15, 90)]
        [TestCase(11, 90)]
        [TestCase(9, 90)]
        [TestCase(5, 90)]
        [TestCase(3, 90)]
        [TestCase(9, 100)]
        [TestCase(9, 50)]
        [TestCase(9, 10)]
        public void VerifyStartInstructionsAreSent(int arenaSize, int flipOdds)
        {
            var botAName = "Bot 1";
            var botBName = "Bot 2";


            var bot1 = new Mock<Bot>(Direction.Right, botAName);
            bot1.Setup(x => x.GetMove()).Returns(new Logic.BotMove(bot1.Object, Logic.Move.AttackWithAxe));
            bot1.Setup(x => x.SendStartInstruction(botBName, arenaSize, flipOdds)).Verifiable();
            var bot2 = new Mock<Bot>(Direction.Left, botBName);
            bot2.Setup(x => x.GetMove()).Returns(new Logic.BotMove(bot2.Object, Logic.Move.AttackWithAxe));
            bot2.Setup(x => x.SendStartInstruction(botAName, arenaSize, flipOdds)).Verifiable();
            var gameClass = new GameClass(bot1.Object, bot2.Object, 100, 10, flipOdds, 10, arenaSize);

            gameClass.CommenceBattle((arena, gc, a, b, ma, mb, o) => { }, 1, 10, new Options());


            bot1.Verify(x => x.SendStartInstruction(botBName, arenaSize, flipOdds), Times.Once());
            bot2.Verify(x => x.SendStartInstruction(botAName, arenaSize, flipOdds), Times.Once());
        }
    }
}
