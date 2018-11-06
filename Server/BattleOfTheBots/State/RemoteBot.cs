using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.HTTP;
using BattleOfTheBots.Logic;

namespace BattleOfTheBots.State
{
    public class RemoteBot : Bot
    {
        public string Url { get; set; }

        public RemoteBot(Direction direction, string url, string Name) 
            : base(direction, Name)
        {
            this.Url = url;
        }

        public override BotMove GetMove()
        {
            var move = HTTPUtility.GetMove(this.Url);
            return new BotMove(this, move);
        }

        public override void PostOpponentsMove(Move move)
        {
            var moveStr = Enum.GetName(typeof(Move), move);
            HTTPUtility.PostMove(this.Url, moveStr);
        }

        public override void PostFlipped ()
        {
            HTTPUtility.PostFlipped(this.Url);
        }

        public override void PostOpponentFlipped()
        {
            HTTPUtility.PostOpponentFlipped(this.Url);
        }

        public override string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            var charDirection = Enum.GetName(typeof(Direction), this.DesiredDirection).ToLower().ToCharArray()[0];

            return HTTPUtility.SendStartInstruction(this.Url,
                opponentBotName,
                this.Position,
                this.Health,
                arenaSize,
                this.NumberOfFlipsRemaining,
                flipOdds,
                FlameThrowerFuelRemaining,
                charDirection);
        }
    }
}
