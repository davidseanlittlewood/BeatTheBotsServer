using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.AI
{
    public class BashBot : Bot
    {
        public List<Move> PreviousMoves { get; set; }        

        public BashBot(Direction direction)
            : base(direction, "Mr Bash")
        {
            this.PreviousMoves = new List<Move>();
        }

        public override BotMove GetMove()
        {
            var rand = new Random();
            Move move;
            if (this.Opponent != null)
            {                
                if (this.IsFlipped && NumberOfFlipsRemaining > 0)
                {
                    move = Move.Flip;
                }

                if (PositionHelpers.AreSideBySide(this, this.Opponent))
                {
                    if (this.Opponent.IsFlipped)
                    {
                        move = Move.AttackWithAxe;
                    }
                    else
                    {
                        if (this.Health > (this.Opponent?.Health ?? 30) && (rand.Next(100) > 50))
                        {
                            move = Move.Shunt;
                        }
                        else
                        {
                            move = Move.AttackWithAxe;
                        }
                    }
                }
                else if (PositionHelpers.AreSeperatedByOneSpace(this, this.Opponent))
                {
                    move = Move.MoveForwards;
                }
                else if (PositionHelpers.AreSeperatedByMoreThanOneSpace(this, this.Opponent))
                {
                    move = Move.MoveForwards;
                }
                else // no idea what's happening! Maybe we've won.. let's just wave the axe around
                {
                    move = Move.AttackWithAxe;
                }
            }
            else // if for whatever reason the axe hasn't been set
            {
                move = Move.AttackWithAxe;
            }

            return new BotMove(this, move);
        }

        public override void PostOpponentsMove(Move move)
        {
            PreviousMoves.Add(move);
        }

        public override string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            return "OK";
        }
    }
}
