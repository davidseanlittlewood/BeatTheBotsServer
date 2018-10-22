using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample
{
    public enum Move
    {
        /// <summary>
        /// The Move provided if an error occoured
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Attempt to flip your opponent
        /// </summary>
        Flip,

        /// <summary>
        /// Charge forward at your opponent in an attempt to push them backwards
        /// </summary>
        Shunt,

        /// <summary>
        /// Attack your opponent with an axe
        /// </summary>
        AttackWithAxe,

        /// <summary>
        /// Move forwards steadily
        /// </summary>
        MoveForwards,

        /// <summary>
        /// Move backwards cautiously
        /// </summary>
        MoveBackwards
    }
}
