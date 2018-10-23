using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.AI
{
    /// <summary>
    /// This bot will turn around and hurl itself off the nearest edge
    /// </summary>
    public class LemmingBot : Bot
    {
        public LemmingBot(Direction direction) : base(direction, "Lemming Bot")
        {
        }

        public override BotMove GetMove()
        {
            return new BotMove(this, Move.MoveBackwards);
        }

        public override void PostOpponentsMove(Move move)
        {
        }

        public override string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            return "OK";
        }
    }
}