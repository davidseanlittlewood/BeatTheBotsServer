using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.Logic;

namespace BattleOfTheBots.State
{
    public class Arena
    {
        public Bot Winner { get; set; }

        public IEnumerable<Bot> Bots { get; set; }

        public int NumberOfSquares { get; set; }
        public int AxeDamage { get; }
        public int ShuntDamage { get; }
        public int FlipOdds { get; set; }

        public int ShortRangeFlameThrowerDamage { get; }

        public int LongRangeFlameThrowerDamage { get; }

        public int NumberOfTurnsWithNoDamageToTolerate { get; }
        public VictoryType? VictoryType { get; internal set; }

        public Arena(IEnumerable<Bot> bots,  int numberOfSquares = 9, int axeDamage = 10, int shuntDamage = 5, int shortRangeFlameThrowerDamage = 20, int longRangeFlameThrowerDamage = 10, int numberOfTurnsWithNoDamageToTolerate = 50, int flipOdds = 100)
        {
            this.Bots = bots;
            this.NumberOfSquares = numberOfSquares;
            this.AxeDamage = axeDamage;
            this.ShuntDamage = shuntDamage;
            this.ShortRangeFlameThrowerDamage = shortRangeFlameThrowerDamage;
            this.LongRangeFlameThrowerDamage = longRangeFlameThrowerDamage;
            this.NumberOfTurnsWithNoDamageToTolerate = numberOfTurnsWithNoDamageToTolerate;
            this.FlipOdds = flipOdds;
        }
    }
}
