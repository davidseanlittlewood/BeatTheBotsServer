using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.Classes
{

    public class BotClass
    {
        private string _lastmove;
        private int _dynamiteRemaining;
        private string _name;
        private int _totalPoints = 0;
        private string _url;

        public BotClass(int dynamite, string name, string url)
        {
            this._dynamiteRemaining = dynamite;
            this._name = name;
            this._url = url;
        }

        public string Name
        {
            get { return this._name; }            
        }

        public string Url
        {
            get { return this._url; }
        }

        internal string LastMove
        {
            get
            {
                return this._lastmove;
            } 

            set
            {
                _lastmove = value;

                if (_lastmove == "DYNAMITE")
                {
                    if (_dynamiteRemaining < 1)
                    {
                        this._lastmove = "WATERBOMB";
                    }
                    else
                    {
                        _dynamiteRemaining--;
                    }
                }

            }
        }

        public int DynamiteRemaining
        {
            get { return _dynamiteRemaining; }
        }

        public int TotalPoints { get { return this._totalPoints; }}

        public void AddWin(int points)
        {
            this._totalPoints = this._totalPoints + points;
        }
    }

}