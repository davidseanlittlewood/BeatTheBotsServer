using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleOfTheBots.HTTP;
using System.Windows.Forms;


namespace BattleOfTheBots.Classes
{
    public class GameClass
    {
        private readonly int _maxRounds;
        private readonly int _pointsToWin;
        private readonly int _dynamite;

        private string _winner = string.Empty;

        private readonly BotClass _bot1;
        private readonly BotClass _bot2;

        public delegate void UpdateMatchProgressDelegate(GameClass currentGame, int gameCount, int totalGames);
        public event UpdateMatchProgressDelegate UpdateCurrentMatch;
          
        public string Winner { get { return _winner; }}
        public string Bot1Name { get { return this._bot1.Name; } }
        public string Bot2Name { get { return this._bot2.Name; } }

        public int Bot1Points { get { return this._bot1.TotalPoints; } }
        public int Bot2Points { get { return this._bot2.TotalPoints; } }

        public GameClass(BotClass bot1, BotClass bot2, int maxRounds, int pointToWin, int dynamite)
        {
            this._maxRounds = maxRounds;
            this._pointsToWin = pointToWin;
            this._dynamite = dynamite;

            this._bot1 = bot1;
            this._bot2 = bot2;

        }

        public void CommenceBattle(UpdateMatchProgressDelegate updateCurrentMatch, int gameCount, int totalGames)
        {

            updateCurrentMatch += this.UpdateCurrentMatch;

            if (HTTPUtility.SendStartInstruction(this._bot1, this._bot2, this._pointsToWin, this._maxRounds, this._dynamite) == "failed")
            {
                AbandonBattle(this._bot2); 
                return;
            }

            if (HTTPUtility.SendStartInstruction(this._bot2, this._bot1,  this._pointsToWin, this._maxRounds, this._dynamite) == "failed")
            {
                AbandonBattle(this._bot1); 
                return;
            }


            int points = 1;
            bool maxPointsReached = false;

            for (int rcount = 1; rcount < this._maxRounds && !maxPointsReached; rcount++)
            {
                this._bot1.LastMove = HTTPUtility.GetMove(this._bot1);
                this._bot2.LastMove = HTTPUtility.GetMove(this._bot2);

                HTTPUtility.PostMove(this._bot1, this._bot2.LastMove);
                HTTPUtility.PostMove(this._bot2,this._bot1.LastMove);

                if (this._bot1.LastMove == "failed")
                {
                    AbandonBattle(this._bot2);
                    return;
                }
                
                if (this._bot2.LastMove == "failed")
                {
                    AbandonBattle(this._bot1); 
                    return;
                }

                switch (WinningMove(this._bot1.LastMove, this._bot2.LastMove))
                {
                    case 0:
                    {
                        points++;
                        break;
                    }
                    case 1:
                    {
                        this._bot1.AddWin(points);
                        points = 1;
                        break;
                    }
                    case 2:
                    {
                        this._bot2.AddWin(points);
                        points = 1;
                        break;
                    }
                    default:
                        break;
                }
                maxPointsReached = ((this._bot1.TotalPoints >= this._pointsToWin) ||
                                    (this._bot2.TotalPoints >= this._pointsToWin));

                updateCurrentMatch(this, gameCount, totalGames);
            }
            
            RegisterBattleWinner();            
        }

        private void AbandonBattle(BotClass winningBot)
        {
            this._winner = winningBot.Name;
            winningBot.AddWin(this._pointsToWin);

            RegisterBattleWinner();
        }

        private void RegisterBattleWinner()
        {
            if (this._bot1.TotalPoints > this._bot2.TotalPoints)
            {
                this._winner = this._bot1.Name;
            }
            else if (this._bot2.TotalPoints > this._bot1.TotalPoints)
            {
                this._winner = this._bot2.Name;
            }
            else
            {
                this._winner = "tie";
            }

        }


        private int WinningMove(string move1, string move2)
        {
            if (move1 == move2)
                return 0;

            switch (move1)
            {
                case "DYNAMITE":
                    {
                        if (move2 == "WATERBOMB")
                            return 2;
                        else
                            return 1;
                    }

                case "ROCK":
                    {
                        if (move2 == "DYNAMITE" || move2 == "PAPER")
                            return 2;
                        else
                            return 1;
                    }
                case "SCISSORS":
                    {
                        if (move2 == "DYNAMITE" || move2 == "ROCK")
                            return 2;
                        else
                            return 1;
                    }
                case "PAPER":
                    {
                        if (move2 == "DYNAMITE" || move2 == "SCISSORS")
                            return 2;
                        else
                            return 1;
                    }
                case "WATERBOMB":
                    {
                        if (move2 == "DYNAMITE")
                            return 1;
                        else
                            return 2;
                    }

            }

            return 0;
        }

    }
}
