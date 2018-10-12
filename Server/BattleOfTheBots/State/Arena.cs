using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.State
{
    public class Arena
    {
        public IEnumerable<Bot> Bots { get; set; }

        public int NumberOfSquares { get; }
        public int AxeDamage { get; }

        public Arena(IEnumerable<Bot> bots, int numberOfSquares = 9, int axeDamage = 10)
        {
            this.NumberOfSquares = numberOfSquares;
            this.AxeDamage = axeDamage;
        }
    }
}
