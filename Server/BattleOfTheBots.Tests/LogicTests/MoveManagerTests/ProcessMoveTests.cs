using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using NUnit.Framework;
using System;
using System.Linq;

namespace BattleOfTheBots.Tests.LogicTests.MoveManagerTests
{
    public class ProcessMoveTests : CommonTestBase
    {

        [TestCase(Move.MoveForwards, Move.MoveForwards, 3, 4, TestName = "When both Bots move forward then neither will move")]        
        [TestCase(Move.MoveBackwards, Move.MoveBackwards, 2, 5, TestName = "When both Bots move backwards then both will move back a space")]
        [TestCase(Move.MoveForwards, Move.MoveBackwards, 4, 5, TestName = "When Bot A moves forwards and B moves backwards then both will move to the right")]
        [TestCase(Move.MoveBackwards, Move.MoveForwards, 2, 3, TestName = "When Bot A moves backwards and B moves forwards then both will move to the left")]
        [TestCase(Move.AttackWithAxe, Move.MoveForwards, 3, 4, 100, 90, TestName = "When Bot A attacks with an axe while B moves forwards then there will be no impact on A but B will remain still and take damage")]
        [TestCase(Move.MoveForwards, Move.AttackWithAxe, 3, 4, 90, 100, TestName = "When Bot A moves forward while B attacks with an axe then there will be no impact on B but A will remain still and take damage")]
        [TestCase(Move.AttackWithAxe, Move.AttackWithAxe, 3, 4, 90, 90, TestName = "When both Bots attack with axes then both will take damage")]
        [TestCase(Move.AttackWithAxe, Move.MoveBackwards, 3, 5, 100, 100, TestName = "When Bot A attacks with an Axe but Bot B moves backwards then B will move to the right and remain undamaged")]
        [TestCase(Move.MoveBackwards, Move.AttackWithAxe, 2, 4, 100, 100, TestName = "When Bot A moves backwards but Bot B attacks with an axe then A will move to the left and remain undamaged")]
        [TestCase(Move.Shunt, Move.AttackWithAxe, 4, 5, 85, 100, TestName = "When Bot A shunts and Bot B attacks with an axe then B will be pushed right but A will take damage from both the axe and the shunt")]
        [TestCase(Move.AttackWithAxe, Move.Shunt, 2, 3, 100, 85, TestName = "When Bot A attacks with an axe and Bot B shunts then A will be pushed left but B will take damage from both the shunt and the axe")]
        [TestCase(Move.Shunt, Move.MoveForwards, 4, 5, 95, 100, TestName = "When Bot A shunts and Bot B moves forward then Bot A will take damage and move right and Bot B will be pushed right")]
        [TestCase(Move.MoveForwards, Move.Shunt, 2, 3, 100, 95, TestName = "When Bot A moves forward and Bot B shunts then Bot A will be pushed left, Bot B will take damage and move left")]
        [TestCase(Move.Shunt, Move.MoveBackwards, 4, 5, 100, 100, TestName = "When Bot A shunts and Bot B moves backwards then both Bots will move to the right and no damage is taken")]
        [TestCase(Move.MoveBackwards, Move.Shunt, 2, 3, 100, 100, TestName = "When Bot A moves backwards and Bot B shunts then both will move to the left but no damage is taken")]
        [TestCase(Move.Shunt, Move.Shunt, 3, 4, 95, 95, TestName = "When both Bots shunt then neither will move but both will take damage")]        
        [TestCase(Move.Flip, Move.Flip, 3, 4, 100, 100, true, true, TestName = "When both Bots flip then both Bots will be turned upside down")]
        [TestCase(Move.Flip, Move.Shunt, 2, 5, 100, 95, false, true, TestName = "When Bot A flips and Bot B shunts then Bot B will be damanged, turned upside down, and moved right a space and Bot A pushed back a space")]
        [TestCase(Move.Flip, Move.AttackWithAxe, 3, 4, 100, 100, false, true, TestName = "When Bot A flips and Bot B attacks with an axe then Bot A will not be damaged because Bot B will be flipped")]
        [TestCase(Move.Flip, Move.MoveForwards, 3, 4, 100, 100, false, true, TestName = "When Bot A flips and Bot B moves forward then Bot A will be unaffected but Bot B will be flipped")]
        [TestCase(Move.Flip, Move.MoveBackwards, 3, 5, 100, 100, false, false, TestName = "When Bot A flips and Bot B moves backwards then B will move to the right")]
        [TestCase(Move.Shunt, Move.Flip, 2, 5, 95, 100, true, false, TestName = "When Bot A shunts and Bot B flips then B will be move backwards but A will be damaged, flipped, and moved to the left")]
        [TestCase(Move.AttackWithAxe, Move.Flip, 3, 4, 100, 100, true, false, TestName = "When Bot A attacks with an axe and Bot B flips then A will flipped before bot B is damaged")]
        [TestCase(Move.MoveForwards, Move.Flip, 3, 4, 100, 100, true, false, TestName = "When Bot A moves forward and Bot B flips then Bot B will be turned over")]
        [TestCase(Move.MoveBackwards, Move.Flip, 2, 4, 100, 100, false, false, TestName = "When Bot A moves backward and bot B flips then bot A will move left")]
        public void CheckMove(Move botAMove, Move botBMove, int botAExpectedPosition, int botBExpectedPosition, int botAExpectedHealth = 100, int botBExpectedHealth = 100, bool expectedFlipAStatus = false, bool expectedFlipBStatus = false)
        {
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botAMove), new BotMove(this.LastBot, botBMove));

            Assert.AreEqual(botAExpectedPosition, this.FirstBot.Position, "Bot A was in an incorrect position");
            Assert.AreEqual(botBExpectedPosition, this.LastBot.Position, "Bot B was in an incorrect position");

            Assert.AreEqual(botAExpectedHealth, this.FirstBot.Health, "Bot A has an incorrect health");
            Assert.AreEqual(botBExpectedHealth, this.LastBot.Health, "Bot B has an incorrect health");

            Assert.AreEqual(expectedFlipAStatus, this.FirstBot.IsFlipped, "Bot A is incorrectly flipped");
            Assert.AreEqual(expectedFlipBStatus, this.LastBot.IsFlipped, "Bot B is incorrectly flipped");
            Assert.IsNull(this.Arena.Winner);
        }

        [TestCase(Move.MoveForwards, 100, TestName = "When both Bots move forward with a space between them then neither will move")]
        [TestCase(Move.Shunt, 100, TestName = "When both Bots shunt with a space between them then neither will move and neither will take damage")]
        public void CheckMoveOverCompetingSpace(Move botMove, int expectedHealth)
        {
            this.FirstBot.Position = 3;
            this.LastBot.Position = 5;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botMove), new BotMove(this.LastBot, botMove));

            Assert.AreEqual(3, this.FirstBot.Position, "Bot A was in an incorrect position");
            Assert.AreEqual(5, this.LastBot.Position, "Bot B was in an incorrect position");

            Assert.AreEqual(expectedHealth, this.FirstBot.Health, "Bot A has an incorrect health");
            Assert.AreEqual(expectedHealth, this.LastBot.Health, "Bot B has an incorrect health");

            Assert.IsFalse(this.FirstBot.IsFlipped, "Bot A is incorrectly flipped");
            Assert.IsFalse(this.LastBot.IsFlipped, "Bot B is incorrectly flipped");
            Assert.IsNull(this.Arena.Winner);
        }

        [TestCase(TestName = "Verifies that nothing happens when the two bots are not in contact")]
        public void CheckThatNothingHappens()
        {            
            for (int botAPosition = 0; botAPosition <= this.Arena.NumberOfSquares - 2; botAPosition++)
            {
                int botBPosition = botAPosition + 2;
                var allValues = Enum.GetValues(typeof(Move));

                foreach (Move botAMove in allValues)
                {
                    foreach (Move botBMove in allValues)
                    {
                        FirstBot.Position = botAPosition;
                        LastBot.Position = botBPosition;


                        // if we're making an advancing move for either bot then slide them back a little to create space
                        if (botAMove == Move.MoveForwards || botAMove == Move.Shunt)
                        {
                            FirstBot.Position--;
                        }
                        if (botBMove == Move.MoveForwards || botBMove == Move.Shunt)
                        {
                            LastBot.Position++;
                        }

                        // if either bot are off the board then skip this iteration
                        if(FirstBot.Position < 0 || LastBot.Position + 1 > Arena.NumberOfSquares)
                        {
                            continue;
                        }
                        

                        this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, botAMove), new BotMove(LastBot, botBMove));

                        var status = $"BotA: {botAPosition}/{botAMove} and BotB: {botBPosition}/{botBMove}";
                        Assert.AreEqual(100, this.FirstBot.Health, $"The first bot was damaged {status}");
                        Assert.AreEqual(100, this.LastBot.Health, $"The second bot was damaged {status}");
                    }
                }
            }
        }

        [TestCase(Move.MoveBackwards, Move.MoveForwards, 0, 4, false, TestName = "When Bots A drives off the edge Bot B wins")]
        [TestCase(Move.MoveForwards, Move.MoveBackwards, 3, 8, true, TestName = "When Bots B drives off the edge Bot B wins")]
        [TestCase(Move.AttackWithAxe, Move.MoveForwards, 4, 5, true, 5, 5, TestName = "When Bots A scores crucial damage on Bot B then Bot A wins")]
        [TestCase(Move.MoveForwards, Move.AttackWithAxe, 4, 5, false, 5, 5, TestName = "When Bots B scores crucial damage on Bot A then Bot B wins")]
        public void EndGame(Move botAMove, Move botBMove, int botAPosition, int botBPosition, bool winnerIsBotA, int botAHealth = 100, int botBHealth = 100)
        {
            var expectedWinner = winnerIsBotA ? this.FirstBot : this.LastBot;
            this.FirstBot.Position = botAPosition;
            this.LastBot.Position = botBPosition;
            this.FirstBot.Health = botAHealth;
            this.LastBot.Health = botBHealth;

            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, botAMove), new BotMove(this.LastBot, botBMove));

            Assert.AreEqual(expectedWinner, this.Arena.Winner);
        }

        [TestCase(TestName ="Cannot flip opponent if no flips are left")]
        public void CheckFlipFailsWithNoFuel()
        {
            this.FirstBot.NumberOfFlipsRemaining = 0;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, Move.Flip), new BotMove(this.LastBot, Move.MoveForwards));

            Assert.IsFalse(this.LastBot.IsFlipped, "Bot B shouldn't be flipped if Bot A has no remaining flips");
        }

        [TestCase(TestName = "Cannot onto wheels if no flips are left")]
        public void CheckReflipFailsWithNoFuel()
        {
            this.FirstBot.IsFlipped = true;
            this.FirstBot.NumberOfFlipsRemaining = 0;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(this.FirstBot, Move.Flip), new BotMove(this.LastBot, Move.MoveForwards));

            Assert.IsTrue(this.FirstBot.IsFlipped, "Bot A shouldn't be able to right itself if it has no remaining flips");
        }

        [TestCase(TestName ="When Bot A is upside down it can flip back onto it's wheels")]
        public void AFlipBackOntoWheels()
        {            
            FirstBot.IsFlipped = true;
            FirstBot.NumberOfFlipsRemaining = 1;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, Move.Flip), new BotMove(LastBot, Move.MoveForwards));

            Assert.IsFalse(FirstBot.IsFlipped, "Bot A should have been able to flip itself back onto it's wheels");
            Assert.AreEqual(0, FirstBot.NumberOfFlipsRemaining, "Bot A should used a flip usage righting itself");
            Assert.AreEqual(5, LastBot.NumberOfFlipsRemaining, "Bot B shouldn't lose flips when Bot A rights itself");
        }
        
        [TestCase(TestName = "Wehn Bot B is upside down it can flip back onto it's wheels")]
        public void BFlipBackOntoWheels()
        {
            LastBot.IsFlipped = true;
            LastBot.NumberOfFlipsRemaining = 1;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, Move.MoveForwards), new BotMove(LastBot, Move.Flip));

            Assert.IsFalse(LastBot.IsFlipped, "Bot B should have been able to flip itself back onto it's wheels");
            Assert.AreEqual(0, LastBot.NumberOfFlipsRemaining, "Bot B should used a flip usage righting itself");
            Assert.AreEqual(5, FirstBot.NumberOfFlipsRemaining, "Bot A shouldn't lose flips when Bot B rights itself");
        }



        [TestCase(TestName = "Two Bots, both flipped and side by side can flip back onto their wheels")]
        public void BothFlipBackOntoWheels()
        {
            FirstBot.IsFlipped = true;
            LastBot.IsFlipped = true;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, Move.Flip), new BotMove(LastBot, Move.Flip));

            Assert.IsFalse(FirstBot.IsFlipped, "Bot A should have been able to flip itself back onto it's wheels");
            Assert.IsFalse(LastBot.IsFlipped, "Bot B should have been able to flip itself back onto it's wheels");

            Assert.AreEqual(4, FirstBot.NumberOfFlipsRemaining, "Bot A should used a flip usage righting itself");
            Assert.AreEqual(4, LastBot.NumberOfFlipsRemaining, "Bot B should used a flip usage righting itself");
        }

        [TestCase(Move.MoveForwards, TestName = "When Bot A is upside down and bot B strikes it with an axe then it cannot move but it takes double damage")]
        [TestCase(Move.MoveBackwards, TestName = "When Bot A is upside down and bot B strikes it with an axe then it cannot move backwards but it takes double damage")]
        [TestCase(Move.AttackWithAxe, TestName = "When Bot A is upside down and bot B strikes it with an axe then it cannot shunt but it takes double damage")]
        [TestCase(Move.Shunt, TestName = "When Bot A is upside down and bot B strikes it with an axe then it cannot shunt but it takes double damage")]
        public void ADoubleDamageUpsideDown(Move botAMove)
        {
            FirstBot.IsFlipped = true;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, botAMove), new BotMove(LastBot, Move.AttackWithAxe));

            Assert.AreEqual(80, FirstBot.Health, "Bot A didn't take double damage");
            Assert.AreEqual(100, LastBot.Health, "Bot B took damage from a flipped opponent");
            Assert.AreEqual(3, FirstBot.Position, "Bot A shouldn't be able to move while it is flipped");
        }

        [TestCase(Move.MoveForwards, TestName = "When Bot B is upside down and bot A strikes it with an axe then it cannot move but it takes double damage")]
        [TestCase(Move.MoveBackwards, TestName = "When Bot B is upside down and bot A strikes it with an axe then it cannot move backwards but it takes double damage")]
        [TestCase(Move.AttackWithAxe, TestName = "When Bot B is upside down and bot A strikes it with an axe then it cannot shunt but it takes double damage")]
        [TestCase(Move.Shunt, TestName = "When Bot B is upside down and bot A strikes it with an axe then it cannot shunt but it takes double damage")]
        public void BDoubleDamageUpsideDown(Move botBMove)
        {
            LastBot.IsFlipped = true;
            this.MoveManager.ProcessMove(this.Arena, new BotMove(FirstBot, Move.AttackWithAxe), new BotMove(LastBot, botBMove));

            Assert.AreEqual(80, LastBot.Health, "Bot B didn't take double damage");
            Assert.AreEqual(100, FirstBot.Health, "Bot A  took damage from a flipped opponent");
            Assert.AreEqual(4, LastBot.Position, "Bot B shouldn't be able to move while it is flipped");
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
