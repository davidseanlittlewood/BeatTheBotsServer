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
            MoveBots(arena, botA, botB);
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
                    botA.Bot.NumberOfFlipsRemaining--;
                    botB.Bot.IsFlipped = true;
                }
                if (botB.Move == Move.Flip)
                {
                    botA.Bot.IsFlipped = true;
                    botB.Bot.NumberOfFlipsRemaining--;
                }
            }

            // flip yourself back over
            if (botA.Move == Move.Flip && botA.Bot.IsFlipped)
            {
                botA.Bot.NumberOfFlipsRemaining--;
                botB.Bot.IsFlipped = true;
            }
            if (botB.Move == Move.Flip && botB.Bot.IsFlipped)
            {
                botA.Bot.IsFlipped = true;
                botB.Bot.NumberOfFlipsRemaining--;
            }
        }

        private void ProcessAxeDamage(Arena arena, BotMove botA, BotMove botB)
        {
            if(AreSideBySide(botA, botB)) // only deal damage if they're side by side
            {
                if (botA.Move == Move.AttackWithAxe) botB.Bot.Health -= arena.AxeDamage;
                if (botB.Move == Move.AttackWithAxe) botA.Bot.Health -= arena.AxeDamage;
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

        private void MoveBots(Arena arena, BotMove botA, BotMove botB)
        {
            if(botA.Move == Move.MoveForwards)
            {
                // We can move bot A forward UNLESS bot B is there (or wants to be there)
                if(botB.Bot.Position == botA.Bot.Position + 1 && botB.Move != Move.MoveBackwards) // Bot B is in front and not moving back
                {
                    // Bot A can't move because Bot B is in the way
                }

                if(botB.Bot.Position == botA.Bot.Position + 2 && botB.Move == Move.MoveForwards) // Bot B is a space away but both want to move into the same space
                {
                    return; // abort - neither can move!
                }
            }
            else if(botA.Move == Move.MoveBackwards) // you can always move backwards (although sometimes it may not be a great idea)
            {
                botA.Bot.Position--;
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
                botB.Bot.Position++;
            }
        }
    }
}
