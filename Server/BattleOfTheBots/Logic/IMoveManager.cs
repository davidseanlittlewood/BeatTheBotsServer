using BattleOfTheBots.State;

namespace BattleOfTheBots.Logic
{
    public interface IMoveManager
    {
        void ProcessMove(Arena arena, BotMove botA, BotMove botB);
    }
}