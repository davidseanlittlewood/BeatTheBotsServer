using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample.Bots
{
    public class RandomBot : BotBaseClass
    {
        public string OpponentName { get; set; }
        public string LastOpponentsMove { get; set; }
        public bool Flipped { get; set; }
        public int Health { get; set; }
        public int Flips { get; set; }
        public int Fuel { get; set; }
        public int FlipOdds { get; set; }
        public int ArenaSize { get; set; }
        public char Direction { get; set; }


        public override string GetMove()
        {
            return GetRandomResponse();
        }

        public string GetRandomResponse()
        {

            Random random = new System.Random(Environment.TickCount);
            int rnd = random.Next(6);
            switch (rnd)
            {
                case 0:
                    {
                        return "FORWARD";
                    }
                case 1:
                    {
                        return "BACK";
                    }
                case 2:
                    {
                        return "HIT";
                    }
                case 3:
                    {
                        return "SHUNT";
                    }
                case 4:
                    {
                        Fuel--;
                        return "FLAME";
                    }
                default:
                    {
                        Flips--;
                        return "FLIP";
                    }
            }
        }
    }
}
