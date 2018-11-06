using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Logic
{
    public class MoveManager : IMoveManager
    {        
        public void ProcessMove(Arena arena, BotMove botA, BotMove botB)
        {
            CheckBotConditions(arena, botA, botB);
            ProcessMovements(arena, botA, botB);            
            ProcessFlips(arena, botA, botB);
            ProcessWeaponDamage(arena, botA, botB);
            CheckForVictory(arena, botA, botB);
        }

        private void CheckBotConditions(Arena arena, BotMove botA, BotMove botB)
        {
            if (botA.Bot.DesiredDirection == botB.Bot.DesiredDirection)
            {
                throw new InvalidOperationException("Both bots want to go in the same direction!");
            }

            if(botA.Bot.Position == botB.Bot.Position)
            {
                throw new InvalidOperationException("Both bots are on the same square");
            }

            var leftee = Math.Min(botA.Bot.Position, botB.Bot.Position);
            var rightee = Math.Max(botA.Bot.Position, botB.Bot.Position);            

            if(leftee != arena.Bots.Single(b => b.DesiredDirection == Direction.Right).Position)
            {
                throw new InvalidOperationException("The left most bot should face right");
            }

            if (rightee != arena.Bots.Single(b => b.DesiredDirection == Direction.Left).Position)
            {
                throw new InvalidOperationException("The right most bot should face left");
            }
        }

        private void ProcessFlips(Arena arena, BotMove botA, BotMove botB)
        {
            // We need to work out who's flipped BEFORE we actually do the flipping, because you can't flip when you're flipped and both may flip at the same time
            bool aWasFlippedThisTurn = false;
            bool bWasFlippedThisTurn = false;

            if (PositionHelpers.AreSideBySide(botA.Bot, botB.Bot)) // you can only flip if they're side by side
            {
                if (botA.Move == Move.Flip                    
                    && botA.Bot.NumberOfFlipsRemaining > 0)
                {

                    botA.Bot.NumberOfFlipsRemaining--;

                    if (FlipSuccess(arena.FlipOdds) 
                        && !botA.Bot.IsFlipped)
                    {
                        bWasFlippedThisTurn = true;
                        if (botB.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                        {
                            TheBotMovesRight(botB); // we're moving twice here to counteract the effect of the shunt
                            TheBotMovesRight(botB);
                        }
                    }
                }
                if (botB.Move == Move.Flip
                    && botB.Bot.NumberOfFlipsRemaining > 0)
                {
                    botB.Bot.NumberOfFlipsRemaining--;

                    if (FlipSuccess(arena.FlipOdds)
                        && !botB.Bot.IsFlipped)
                    {
                        aWasFlippedThisTurn = true;
                        if (botA.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                        {
                            TheBotMovesLeft(botA);
                            TheBotMovesLeft(botA);
                        }
                    }
                }
            }

            
            if (bWasFlippedThisTurn) // if the above determined theyneeded to be flipped then flip them
            {
                TheBotIsFlippedOntoItsBack(botA, botB);
            }
            else // if they weren't flipped then we can check if they are flipped and want to self-right
            {
                if (botB.Move == Move.Flip
                && botB.Bot.IsFlipped
                && !bWasFlippedThisTurn
                && botB.Bot.NumberOfFlipsRemaining > 0)
                {
                    TheBotIsFlippedOntoItsWheels(botB);
                }
            }


            if (aWasFlippedThisTurn)
            {
                TheBotIsFlippedOntoItsBack(botB, botA);
            }
            else
            {
                if (botA.Move == Move.Flip
                    && botA.Bot.IsFlipped
                    && !aWasFlippedThisTurn
                    && botA.Bot.NumberOfFlipsRemaining > 0)
                {
                    TheBotIsFlippedOntoItsWheels(botA);
                }
            }
        }

        private bool FlipSuccess(int flipOdds)
        {
            if (flipOdds == 0)
                return false;

            Random random = new System.Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(100/flipOdds);
            if (rnd == 0)
                return true;
            else
                return false;
        }

        private void ProcessWeaponDamage(Arena arena, BotMove botA, BotMove botB)
        {
            if (PositionHelpers.AreSideBySide(botA.Bot, botB.Bot)) // only deal damage if they're side by side
            {
                if (botB.Move == Move.AttackWithAxe && !botB.Bot.IsFlipped)
                {
                    var damage = botA.Bot.IsFlipped 
                        ? (2 * arena.AxeDamage)
                        : arena.AxeDamage;
                    TheBotTakesDamage(botA, damage);
                }

                if (botA.Move == Move.AttackWithAxe && !botA.Bot.IsFlipped)
                {
                    var damage = botB.Bot.IsFlipped
                        ? (2 * arena.AxeDamage)
                        : arena.AxeDamage;
                    TheBotTakesDamage(botB, damage);
                }                                    
            }


            if (botA.Move == Move.FlameThrower && botA.Bot.FlameThrowerFuelRemaining > 0)
            {
                botA.Bot.FlameThrowerFuelRemaining--;
                if (!botA.Bot.IsFlipped)
                {                    
                    var damage = 0;
                    if (PositionHelpers.AreSeperatedByOneSpace(botA.Bot, botB.Bot)) damage = arena.LongRangeFlameThrowerDamage;
                    else if (PositionHelpers.AreSideBySide(botA.Bot, botB.Bot)) damage = arena.ShortRangeFlameThrowerDamage;

                    TheBotTakesDamage(botB, damage);
                }
            }

            if (botB.Move == Move.FlameThrower && botB.Bot.FlameThrowerFuelRemaining > 0)
            {
                botB.Bot.FlameThrowerFuelRemaining--;
                if (!botB.Bot.IsFlipped)
                {
                    var damage = 0;
                    if (PositionHelpers.AreSeperatedByOneSpace(botA.Bot, botB.Bot)) damage = arena.LongRangeFlameThrowerDamage;
                    else if (PositionHelpers.AreSideBySide(botA.Bot, botB.Bot)) damage = arena.ShortRangeFlameThrowerDamage;

                    TheBotTakesDamage(botA, damage);
                }
            }
        }


        private void CheckForVictory(Arena arena, BotMove botA, BotMove botB)
        {
            VictoryType victoryType;
            arena.Winner = VictoryHelper.CheckForWinner(arena, botA.Bot, botB.Bot, out victoryType);
            if (victoryType > VictoryType.Draw)
            {
                arena.VictoryType = victoryType;
            }
            else
            {
                arena.VictoryType = null;
            }
        }

        private void ProcessMovements(Arena arena, BotMove botA, BotMove botB)
        {
            var aStartPosition = botA.Bot.Position;
            var bStartPosition = botB.Bot.Position;
            var aMove = botA.Move;
            var bMove = botB.Move;

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
            if (PositionHelpers.AreSeperatedByMoreThanOneSpace(botA.Bot, botB.Bot))
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
            else if(PositionHelpers.AreSeperatedByOneSpace(botA.Bot, botB.Bot))
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
                    var mover = both.Single(b => b.Move == Move.MoveForwards && !b.Bot.IsFlipped);
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

            
            if(botA.Bot.Position == botB.Bot.Position) // Something has gone very very wrong!
            {                
                throw new InvalidOperationException($"An error has occoured, both Bots occupy the same space at the start the data was A({aStartPosition}/{aMove}) and B({bStartPosition}/{bMove}) the positions now are {botA.Bot.Position} and {botB.Bot.Position}");
            }
        }

        #region Helpers

        public bool EitherBotIsShunting(BotMove botA, BotMove botB)
        {
            return (botA.Move == Move.Shunt && !botA.Bot.IsFlipped)
                || (botB.Move == Move.Shunt && !botB.Bot.IsFlipped);
        }

        public bool BothBotsAreShunting(BotMove botA, BotMove botB)
        {
            return botA.Move == Move.Shunt
                && botB.Move == Move.Shunt
                && !botA.Bot.IsFlipped
                && !botB.Bot.IsFlipped;
        }

        public bool IsBotAdvancing(BotMove bot)
        {
            return !bot.Bot.IsFlipped && (bot.Move == Move.MoveForwards || bot.Move == Move.Shunt);
        }

        public bool IsBotWithdrawing(BotMove bot)
        {
            return !bot.Bot.IsFlipped && (bot.Move == Move.MoveBackwards);
        }

        public bool AreBothAdvancing(BotMove botA, BotMove botB)
        {
            return IsBotAdvancing(botA) 
                && IsBotAdvancing(botB) 
                && !botA.Bot.IsFlipped
                && !botB.Bot.IsFlipped;
        }

        public bool IsEitherAdvancing(BotMove botA, BotMove botB)
        {
            return (IsBotAdvancing(botA) && !botA.Bot.IsFlipped)
                || (IsBotAdvancing(botB) && !botB.Bot.IsFlipped);
        }


     

        #endregion

        #region Outcomes

        public void OneBotStealsAnothersSpace(BotMove tortoise, BotMove hare)
        {
            const string errorMessage = "This method can only be used when two bots are separated by a single square";
            if (!PositionHelpers.AreSeperatedByOneSpace(tortoise.Bot, hare.Bot)) throw new InvalidOperationException(errorMessage);

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
            const string errorMessage = "This method can only be used when two bots are side by side and one is shunting";
            if (BothBotsAreShunting(botA, botB)) throw new InvalidOperationException(errorMessage);
            if (!PositionHelpers.AreSideBySide(botA.Bot, botB.Bot)) throw new InvalidOperationException(errorMessage);
            if (botA.Move != Move.Shunt && botB.Move != Move.Shunt) throw new InvalidOperationException(errorMessage);

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
