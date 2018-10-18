using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.State
{
    public class Arena
    {
        public Bot Winner { get; protected set; }

        public IEnumerable<Bot> Bots { get; set; }

        public int NumberOfSquares { get; }
        public int AxeDamage { get; }
        public int ShuntDamage { get; }

        public Arena(IEnumerable<Bot> bots, int numberOfSquares = 9, int axeDamage = 10, int shuntDamage = 5)
        {
            this.NumberOfSquares = numberOfSquares;
            this.AxeDamage = axeDamage;
            this.ShuntDamage = shuntDamage;
        }
    }
}
