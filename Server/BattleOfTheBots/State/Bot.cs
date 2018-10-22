namespace BattleOfTheBots.State
{
    public class Bot
    {

        public string Name { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }

        public int Health { get; set; }

        public int NumberOfFlipsRemaining { get; set; }
        public bool IsFlipped { get; set; }

        public Direction DesiredDirection { get;  protected set; }

        public int FlameThrowerFuelRemaining { get; set; }

        public Bot(Direction direction, string Url, string Name)
        {
            this.Url = Url;
            this.Name = Name;
            this.IsFlipped = false;
            this.DesiredDirection = direction;
        }
            public Bot(Direction direction)
        {
            this.IsFlipped = false;
            this.DesiredDirection = direction;
        }
    }
}