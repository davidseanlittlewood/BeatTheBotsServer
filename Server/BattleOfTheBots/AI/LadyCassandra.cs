using BattleOfTheBots.Logic;
using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.AI
{
    public class LadyCassandra : Bot
    {
        public List<Move> PreviousMoves { get; set; }      
        
        public int? StartingFlips { get; set; }

        public IEnumerable<Move> OpponentsRecentMoves
        {
            get
            {
                var skip = Math.Max(this.PreviousMoves.Count - 5, 0);
                return this.PreviousMoves.Skip(skip);
            }
        }

        public int ArenaSize { get; private set; }

        public LadyCassandra(Direction direction)
            : base(direction, "Lady Cassandra")
        {
            this.PreviousMoves = new List<Move>();
        }

        public override BotMove GetMove()
        {
            if (!this.StartingFlips.HasValue) this.StartingFlips = this.NumberOfFlipsRemaining;
            var rand = new Random();
            Move move;

            if (this.IsFlipped && NumberOfFlipsRemaining > 0)
            {
                move = Move.Flip;

            }
            else
            {

                if (this.Opponent != null)
                {
                    if (PositionHelpers.AreSideBySide(this, this.Opponent))
                    {
                        if (this.Opponent.IsFlipped)
                        {
                            move = Move.AttackWithAxe;
                        }
                        else
                        {
                            if (IsOpponentLikelyToFlip() && !PositionHelpers.IsWithinXOfEdge(this.Position, this.ArenaSize, 2))
                            {
                                move = Move.MoveBackwards;
                            }
                            else if (IsOpponentLikelyToShunt() && this.NumberOfFlipsRemaining > 1)
                            {
                                move = Move.Flip;
                            }
                            else if (this.FlameThrowerFuelRemaining > 0)
                            {
                                move = Move.FlameThrower;
                            }
                            else
                            {
                                move = Move.AttackWithAxe;
                            }
                        }
                    }
                    else if (PositionHelpers.AreSeperatedByOneSpace(this, this.Opponent))
                    {
                        if (this.FlameThrowerFuelRemaining > 0)
                        {
                            move = Move.FlameThrower;
                        }
                        else if (rand.Next(100) > 20 && this.Health > 30)
                        {
                            move = Move.Shunt;
                        }
                        else
                        { 
                            move = Move.MoveForwards;
                        }
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
            }

            return new BotMove(this, move);
        }

        public bool IsOpponentLikelyToFlip()
        {
            if(this.Opponent.NumberOfFlipsRemaining == 0)
            {
                return false;
            }            

            var trend = IsOpponentTrendingTowards(Move.Flip);
            var pattern = DoesOpponentFavour(Move.Flip);
            var percentageOfFlipsRemainingScore = (int)(this.Opponent.NumberOfFlipsRemaining / this.StartingFlips) * 10;

            var score = 0;
            if (trend) score += 7;
            if (pattern) score += 5;

            score += percentageOfFlipsRemainingScore;
            return score > 10;
        }

        public bool IsOpponentLikelyToShunt()
        {
            if (this.Opponent.NumberOfFlipsRemaining == 0)
            {
                return false;
            }

            var trend = IsOpponentTrendingTowards(Move.Shunt);
            var pattern = DoesOpponentFavour(Move.Shunt);

            var score = 0;
            if (trend) score += 7;
            if (pattern) score += 5;

            return score >= 5;
        }



        public bool IsOpponentTrendingTowards(Move move)
        {
            var proportionOfMoveAllTime = this.PreviousMoves.Count(m => m == move) / (double)this.PreviousMoves.Count;
            var proportionOfRecentMoves = this.OpponentsRecentMoves.Count(m => m == move) / (double)this.OpponentsRecentMoves.Count();
            var availableMoves = Enum.GetValues(typeof(Move));
            var expectedProportion = 1D / availableMoves.Length;

            return proportionOfMoveAllTime > proportionOfRecentMoves;
        }

        public bool DoesOpponentFavour(Move move)
        {
            var proportionOfMoveAllTime = this.PreviousMoves.Count(m => m == move) / (double)this.PreviousMoves.Count;
            var availableMoves = Enum.GetValues(typeof(Move));
            var expectedProportion = 1D / availableMoves.Length;

            return proportionOfMoveAllTime * 1.3 > expectedProportion;
        }

        public override void PostOpponentsMove(Move move)
        {
            PreviousMoves.Add(move);
        }

        public override string SendStartInstruction(string opponentBotName, int arenaSize, int flipOdds)
        {
            this.PreviousMoves = new List<Move>();
            this.ArenaSize = arenaSize;
            return "OK";
        }
    }
}
