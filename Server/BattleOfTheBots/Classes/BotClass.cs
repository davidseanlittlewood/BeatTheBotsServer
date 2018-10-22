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
        private string _name;
        private int _health;
        private int _flips;
        private bool _flipped;
        private int _fuel;
        private string _lastMove;
        private string _url;        

        public BotClass(string name, string url, int health, int flips, int fuel )
        {
            this._name = name;
            this._url = url;
            this._health = health;
            this._flips = flips;
            this._fuel = fuel;
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
                return this._lastMove  ;
            } 
            set
            {
                this._lastMove = value;             
            }
        }

        public int Health
        {
            get { return _health; }
            set { this._health = value ; }
        }
        public bool Flipped
        {
            get { return _flipped; }
            set { this._flipped = value; }
        }
        public void UsedFlip()
        {
            this._flips--;
        }
        public void UsedFuel()
        {
            this._fuel--;
        }

    }

}