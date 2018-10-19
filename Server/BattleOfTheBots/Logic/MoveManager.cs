using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Logic
{
    public class MoveManager
    {        
        public void ProcessMove(Arena arena, BotMove botA, BotMove botB)
        {            
            ProcessMovements(arena, botA, botB);
            ProcessAxeDamage(arena, botA, botB);
            ProcessFlips(arena, botA, botB);            
            CheckForVictory(arena, botA, botB);
        }
       

        private void ProcessFlips(Arena arena, BotMove botA, BotMove botB)
        {
            bool aFlippedThisTurn = false, bFlippedThisTurn = false; // you can't flip yourself back over in the same turn you're flipped
            if (AreSideBySide(botA, botB)) // you can only flip if they're side by side
            {
                if (botA.Move == Move.Flip && botA.Bot.NumberOfFlipsRemaining > 0)
                {
                    TheBotIsFlippedOntoItsBack(botA, botB);
                    bFlippedThisTurn = true;
                    if(botB.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                    {
                        TheBotMovesRight(botB); // we're moving twice here to counteract the effect of the shunt
                        TheBotMovesRight(botB);
                    }
                }
                if (botB.Move == Move.Flip && botB.Bot.NumberOfFlipsRemaining > 0)
                {
                    TheBotIsFlippedOntoItsBack(botB, botA);
                    aFlippedThisTurn = true;
                    if (botA.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                    {
                        TheBotMovesLeft(botA);
                        TheBotMovesLeft(botA);
                    }
                }
            }

            // flip yourself back over
            if (botA.Move == Move.Flip
                && botA.Bot.IsFlipped
                && !aFlippedThisTurn
                && botA.Bot.NumberOfFlipsRemaining > 0)
            {
                TheBotIsFlippedOntoItsWheels(botA);
            }
            if (botB.Move == Move.Flip 
                && botB.Bot.IsFlipped 
                && !bFlippedThisTurn 
                && botB.Bot.NumberOfFlipsRemaining > 0)
            {
                TheBotIsFlippedOntoItsWheels(botB);
            }
        }

        private void ProcessAxeDamage(Arena arena, BotMove botA, BotMove botB)
        {
            if (AreSideBySide(botA, botB)) // only deal damage if they're side by side
            {
                if (botB.Move == Move.AttackWithAxe) TheBotTakesDamage(botA, arena.AxeDamage);
                if (botA.Move == Move.AttackWithAxe) TheBotTakesDamage(botB, arena.AxeDamage);
            }
        }


        private void CheckForVictory(Arena arena, BotMove botA, BotMove botB)
        {
            if (botA.Bot.Position < 0) arena.Winner = botB.Bot;
            if (botB.Bot.Position >= arena.NumberOfSquares) arena.Winner = botA.Bot;

            if (botA.Bot.Health <= 0) arena.Winner = botB.Bot;
            if (botB.Bot.Health <= 0) arena.Winner = botA.Bot;
        }

        private void ProcessMovements(Arena arena, BotMove botA, BotMove botB)
        {
            // You can't obstruct a withdrawl so do those first
            if(IsBotWithdrawing(botA))
            {
                TheBotWithdraws(botA);
            }
            if (IsBotWithdrawing(botB))
            {
                TheBotWithdraws(botB);
            }


            // If we're seperated by more than one space then there's no issue
            if (AreSeperatedByMoreThanOneSpace(botA, botB))
            {
                if(IsBotAdvancing(botA))
                {
                    TheBotAdvances(botA);
                }

                if (IsBotAdvancing(botB))
                {
                    TheBotAdvances(botB);
                }

                return;
            }
            else if(AreSeperatedByOneSpace(botA, botB))
            {
                if(BothBotsAreShunting(botA, botB)) // both are shunting into an empty space
                {
                    NothingHappens();
                }
                else if(EitherBotIsShunting(botA, botB)) // one is shunting and the other isn't
                {
                    var hare = GetShunter(botA, botB);
                    var tortoise = GetShuntee(botA, botB);
                    OneBotStealsAnothersSpace(tortoise, hare);
                }
                else if(AreBothAdvancing(botA, botB)) // both are moving forward
                {
                    NothingHappens();
                }
                else if(IsEitherAdvancing(botA, botB))
                {
                    var both = new BotMove[] { botA, botB };
                    var mover = both.Single(b => b.Move == Move.MoveForwards);
                    var shaker = both.Single(b => !b.Equals(mover));
                    OneBotStealsAnothersSpace(shaker, mover);
                }
                else // no one is going anywhere
                {
                    NothingHappens();
                }
            }
            else // we're against each other and all withdrawls have been done
            {
                if(BothBotsAreShunting(botA, botB)) // we up against each other and both bots have shunted, no movement but damage on both sides
                {
                    TheBotTakesDamage(botA, arena.ShuntDamage);
                    TheBotTakesDamage(botB, arena.ShuntDamage);
                }
                else if(EitherBotIsShunting(botA, botB)) // we're up against each other and one bot is shunting
                {
                    OneBotShuntsAnother(arena, botA, botB);
                }
                else // they are most likely attacking or doing something else
                {
                    NothingHappens();
                }
            }
            
        }

        #region Helpers

        private bool EitherBotIsShunting(BotMove botA, BotMove botB)
        {
            return botA.Move == Move.Shunt || botB.Move == Move.Shunt;
        }

        private bool BothBotsAreShunting(BotMove botA, BotMove botB)
        {
            return botA.Move == Move.Shunt && botB.Move == Move.Shunt;
        }

        public bool IsBotAdvancing(BotMove bot)
        {
            return bot.Move == Move.MoveForwards || bot.Move == Move.Shunt;
        }

        public bool IsBotWithdrawing(BotMove bot)
        {
            return bot.Move == Move.MoveBackwards;
        }

        public bool AreBothAdvancing(BotMove botA, BotMove botB)
        {
            return IsBotAdvancing(botA) && IsBotAdvancing(botB);
        }

        public bool IsEitherAdvancing(BotMove botA, BotMove botB)
        {
            return IsBotAdvancing(botA) || IsBotAdvancing(botB);
        }

        public bool AreEitherWithdrawing(BotMove botA, BotMove botB)
        {
            return botA.Move == Move.MoveBackwards || botB.Move == Move.MoveBackwards;
        }


        public bool AreSideBySide(BotMove botA, BotMove botB)
        {
            return botA.Bot.Position + 1 == botB.Bot.Position;
        }

        public bool AreSeperatedByOneSpace(BotMove botA, BotMove botB)
        {
            return botA.Bot.Position + 2 == botB.Bot.Position;
        }

        public bool AreSeperatedByMoreThanOneSpace(BotMove botA, BotMove botB)
        {
            return botB.Bot.Position - botA.Bot.Position > 2;
        }

        #endregion

        #region Outcomes

        public void OneBotStealsAnothersSpace(BotMove tortoise, BotMove hare)
        {
            var direction = hare.Bot.DesiredDirection;
            if (direction == Direction.Left)
            {
                TheBotMovesLeft(hare);
            }
            else if (direction == Direction.Right)
            {
                TheBotMovesRight(hare);
            }
        }

        public void OneBotShuntsAnother(Arena arena, BotMove botA, BotMove botB)
        {
            BotMove shunter = GetShunter(botA, botB);
            BotMove shuntee = GetShuntee(botA, botB);

            var direction = shunter.Bot.DesiredDirection;
            TheBotTakesDamage(shunter, arena.ShuntDamage);

            if (direction == Direction.Left)
            {
                TheBotMovesLeft(shunter);
                TheBotMovesLeft(shuntee);
            }
            else if (direction == Direction.Right)
            {
                TheBotMovesRight(shunter);
                TheBotMovesRight(shuntee);
            }
        }

        private BotMove GetShuntee(BotMove botA, BotMove botB)
        {
            return botA.Move != Move.Shunt ? botA : botB;
        }

        private BotMove GetShunter(BotMove botA, BotMove botB)
        {
            return botA.Move == Move.Shunt ? botA : botB;
        }

        private void NothingHappens()
        {
        }

        private void TheBotIsFlippedOntoItsBack(BotMove flipper, BotMove flipee)
        {
            flipper.Bot.NumberOfFlipsRemaining--;
            flipee.Bot.IsFlipped = true;
        }

        private void TheBotIsFlippedOntoItsWheels(BotMove bot)
        {
            bot.Bot.NumberOfFlipsRemaining--;
            bot.Bot.IsFlipped = false;
        }

        private void TheBotTakesDamage(BotMove bot, int amountOfDamage)
        {
            bot.Bot.Health -= amountOfDamage;
        }

        private void TheBotAdvances(BotMove bot)
        {
            if (bot.Bot.DesiredDirection == Direction.Left)
            {
                TheBotMovesLeft(bot);
            }
            else
            {
                TheBotMovesRight(bot);
            }
        }

        private void TheBotWithdraws(BotMove bot)
        {
            if (bot.Bot.DesiredDirection == Direction.Right)
            {
                TheBotMovesLeft(bot);
            }
            else
            {
                TheBotMovesRight(bot);
            }
        }

        private void TheBotMovesLeft(BotMove bot)
        {
            bot.Bot.Position--;
        }

        private void TheBotMovesRight(BotMove bot)
        {
            bot.Bot.Position++;
        }

        #endregion
    }
}
