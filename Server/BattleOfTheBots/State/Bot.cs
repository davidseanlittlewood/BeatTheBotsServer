using System;
using BattleOfTheBots.Logic;

namespace BattleOfTheBots.State
{
    public class Bot
    {

        public string Name { get; set; }
        public int Position { get; set; }

        public int Health { get; set; }

        public int NumberOfFlipsRemaining { get; set; }
        public bool IsFlipped { get; set; }

        public Direction DesiredDirection { get; protected set; }

        public int FlameThrowerFuelRemaining { get; set; }

        public Bot(Direction direction, string name)
        {
            this.Name = name;
            this.IsFlipped = false;
            this.DesiredDirection = direction;
        }

        public Bot(Direction direction)
        {
            this.IsFlipped = false;
            this.DesiredDirection = direction;
        }

        public virtual BotMove GetMove()
        {
            return new BotMove(this, Move.Invalid);
        }

        public virtual void PostOpponentsMove(Move move)
        {
            throw new NotImplementedException();
        }

        public virtual string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            return "failed";
        }
        public override string ToString()
        {
            return $"{this.Name} at position {this.Position}";
        }
    }    
}