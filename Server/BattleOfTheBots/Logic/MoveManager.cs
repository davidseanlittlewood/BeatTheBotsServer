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
            MoveAndShunt(arena, botA, botB);
            ProcessAxeDamage(arena, botA, botB);
            ProcessFlips(arena, botA, botB);            
            CheckForVictory(arena, botA, botB);
        }
       

        private void ProcessFlips(Arena arena, BotMove botA, BotMove botB)
        {
            if (AreSideBySide(botA, botB)) // you can only flip if they're side by side
            {
                if (botA.Move == Move.Flip)
                {
                    TheBotIsFlippedOntoItsBack(botA, botB);
                    
                    if(botB.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                    {
                        TheBotMovesRight(botB);
                        TheBotTakesDamage(botB, arena.ShuntDamage);
                    }
                }
                if (botB.Move == Move.Flip)
                {
                    TheBotIsFlippedOntoItsBack(botB, botA);
                    botA.Bot.IsFlipped = true;
                    botB.Bot.NumberOfFlipsRemaining--;
                    if (botA.Move == Move.Shunt) // if they were shunting a flip then throw them further backwards
                    {
                        TheBotMovesRight(botA);
                        TheBotTakesDamage(botA, arena.ShuntDamage);
                    }
                }
            }

            // flip yourself back over
            if (botA.Move == Move.Flip && botA.Bot.IsFlipped)
            {
                TheBotIsFlippedOntoItsWheels(botA);
            }
            if (botB.Move == Move.Flip && botB.Bot.IsFlipped)
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

        private bool AreSideBySide(BotMove botA, BotMove botB)
        {
            return botA.Bot.Position + 1 == botB.Bot.Position;
        }

        private bool AreSeperatedByOneSpace(BotMove botA, BotMove botB)
        {
            return botA.Bot.Position + 2 == botB.Bot.Position;
        }

        private void CheckForVictory(Arena arena, BotMove botA, BotMove botB)
        {
            if (botA.Bot.Position < 0) arena.Winner = botB.Bot;
            if (botB.Bot.Position >= arena.NumberOfSquares) arena.Winner = botA.Bot;

            if (botA.Bot.Health <= 0) arena.Winner = botB.Bot;
            if (botB.Bot.Health <= 0) arena.Winner = botA.Bot;
        }

        private void MoveAndShunt(Arena arena, BotMove botA, BotMove botB)
        {
            // Check for bots getting in each others' way and things smacking into each other
            if(ProcessCollisions(arena, botA, botB))
            {
                return;
            }


            if (botA.Move == Move.MoveForwards)
            {
                TheBotMovesRight(botA);
            }
            else if(botA.Move == Move.MoveBackwards)
            {
                TheBotMovesLeft(botA);
            }

            if(botB.Move == Move.MoveForwards)
            {
                TheBotMovesLeft(botB);
            }
            else if (botB.Move == Move.MoveBackwards)
            {
                TheBotMovesRight(botB);
            }
        }

        public bool ProcessCollisions(Arena arena, BotMove botA, BotMove botB)
        {
            // firstly you can only have a collison if you're both moving forward!
            if ((botA.Move == Move.MoveForwards || botA.Move == Move.Shunt)
                && (botB.Move == Move.MoveForwards || botB.Move == Move.Shunt))
            {
                if (AreSideBySide(botA, botB))
                {
                    if (botA.Move == Move.MoveForwards) // if BotA is moving forwards
                    {
                        if (botB.Move == Move.MoveForwards) // and BotB is moving forwards
                        {
                            // then don't do anything
                        }
                        else // however if Bot B is shunting
                        {
                            TheBotTakesDamage(botB, arena.ShuntDamage); // then BotB is damaged
                            TheBotMovesLeft(botA); // and both bot A and bot B move left
                            TheBotMovesLeft(botB);
                        }
                    }
                    else // otherwise bot A is shunting
                    {
                        if (botB.Move == Move.MoveForwards) // and BotB is moving forwards
                        {
                            TheBotTakesDamage(botA, arena.ShuntDamage); // then Bot A is damaged
                            TheBotMovesLeft(botA); // and both bot A and bot B move left
                            TheBotMovesLeft(botB);
                        }
                        else // AND Bot B is shunting
                        {
                            TheBotTakesDamage(botA, arena.ShuntDamage); // both bots are damaged
                            TheBotTakesDamage(botB, arena.ShuntDamage); // both bots are damaged
                                                                        // but neither move
                        }
                    }
                }
                else if (AreSeperatedByOneSpace(botA, botB)) // if there's one space between us
                {
                    if (botA.Move == Move.MoveForwards) // if BotA is moving forwards into the space
                    {
                        if (botB.Move == Move.MoveForwards) // and BotB is moving forwards into the space
                        {
                            // then don't do anything (no one gets it)
                        }
                        else // however if Bot B is shunting
                        {
                            TheBotTakesDamage(botB, arena.ShuntDamage); // then BotB is damaged
                            TheBotMovesLeft(botB); // and wins the space
                        }
                    }
                    else // otherwise bot A is shunting
                    {
                        if (botB.Move == Move.MoveForwards) // and BotB is moving forwards
                        {
                            TheBotTakesDamage(botA, arena.ShuntDamage); // then Bot A is damaged
                            TheBotMovesRight(botA); // bot A wins the space                            
                        }
                        else // AND Bot B is shunting
                        {
                            TheBotTakesDamage(botA, arena.ShuntDamage); // both bots are damaged
                            TheBotTakesDamage(botB, arena.ShuntDamage); // both bots are damaged
                                                                        // but neither move into the space
                        }
                    }
                }
                else // otherwise there's a big gap - no collision
                {
                    return false;
                }

                return true;
            }
            else // we're not both moving forward - no collision
            {
                return this.AreSideBySide(botA, botB);
            }            
        }


        public void OneBotStealsAnothersSpace(BotMove tortoise, BotMove hair, Direction direction)
        {
            if (direction == Direction.Left)
            {
                TheBotMovesLeft(hair);
            }
            else if (direction == Direction.Right)
            {
                TheBotMovesRight(hair);
            }
        }

        public void OneBotShuntsAnother(Arena arena, BotMove shunter, BotMove shuntee, Direction direction)
        {
            TheBotTakesDamage(shunter, arena.ShuntDamage);
            if(direction == Direction.Left)
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


        private void NothingHappens(BotMove bot)
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

        private void TheBotMovesLeft(BotMove bot)
        {
            bot.Bot.Position--;
        }

        private void TheBotMovesRight(BotMove bot)
        {
            bot.Bot.Position++;
        }
    }
}
