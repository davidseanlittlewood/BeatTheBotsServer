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
            if(AreSideBySide(botA, botB)) // only deal damage if they're side by side
            {
                TheBotTakesDamage(botA, arena.AxeDamage);
                TheBotTakesDamage(botB, arena.AxeDamage);
            }
        }

        private bool AreSideBySide(BotMove botA, BotMove botB)
        {
            return botA.Bot.Position + 1 == botB.Bot.Position;
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
            if(botA.Move == Move.MoveForwards)
            {
                // We can move bot A forward UNLESS bot B is there (or wants to be there)
                if(botB.Bot.Position == botA.Bot.Position + 1 && botB.Move != Move.MoveBackwards) // Bot B is in front and not moving back
                {
                    // Bot A can't move because Bot B is in the way
                    NothingHappens(botA);
                    NothingHappens(botB);
                }

                if(botB.Bot.Position == botA.Bot.Position + 2 && botB.Move == Move.MoveForwards) // Bot B is a space away but both want to move into the same space
                {
                    NothingHappens(botA);
                    NothingHappens(botB);
                }
            }
            else if(botA.Move == Move.MoveBackwards) // you can always move backwards (although sometimes it may not be a great idea)
            {
                TheBotMovesLeft(botA);
            }

            if (botB.Move == Move.MoveForwards)
            {
                // We can move bot B forward UNLESS bot A is there (or wants to be there)
                if (botA.Bot.Position == botB.Bot.Position - 1 && botA.Move != Move.MoveBackwards) // Bot A is in front and not moving back
                {
                    // Bot B can't move because Bot A is in the way
                }
            }
            else if (botB.Move == Move.MoveBackwards) // you can always move backwards (although sometimes it may not be a great idea)
            {
                TheBotMovesRight(botB);
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
