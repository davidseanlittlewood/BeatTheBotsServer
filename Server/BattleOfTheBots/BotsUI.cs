using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using BattleOfTheBots.UI;
using BattleOfTheBots.State;
using BattleOfTheBots.Logic;
using System.Drawing.Imaging;
using BattleOfTheBots.Classes;

namespace BattleOfTheBots.UIControl
{
    public partial class BotsUI : UserControl
    {

        public List<Point> Precipitation { get; set; }

        public BotsUI()
        {
            InitializeComponent();
            this.Precipitation = new List<Point>();
        }

        int[] _arenaPositions;        

        public void Update(int arenaWidth, BotMove leftBot, BotMove rightBot, int frame, Options options)
        {
            var bitmap = new Bitmap(panelDrawArea.Width, panelDrawArea.Height);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.FillRectangle(Brushes.White, 0, 0, panelDrawArea.Width, panelDrawArea.Height);
                this.DrawArenaFloor(gfx, arenaWidth);
                this.DrawLeftBot(gfx, arenaWidth, leftBot, frame, options);
                this.DrawRightBot(gfx, arenaWidth, rightBot, frame, options);
                if(options.IsChristmas)
                {
                    this.DrawSnow(gfx, arenaWidth);
                    this.DrawFlakes(gfx);
                }
                this.DrawWater(gfx, frame);
            }
            this.DrawImageOnUIPanel(bitmap, new Point(0, 0));
        }

        private void DrawFlakes(Graphics gfx)
        {
            var rand = new Random();
            if(this.Precipitation.Count < 100)
            {
                var numberToAdd = Math.Min(5, 100 - this.Precipitation.Count());
                for(int i = 0; i < numberToAdd; i++)
                {
                    this.Precipitation.Add(new Point(rand.Next(0, this.panelDrawArea.Width), 0));
                }
            }

            var newList = new List<Point>();
            foreach(var flake in this.Precipitation)
            {
                var newX = rand.Next(15) - 5;
                var newY = rand.Next(5) + 10;

                var newPoint = new Point(flake.X + newX, flake.Y + newY);
                if (newPoint.Y < this.panelDrawArea.Height)
                {                    
                    newList.Add(newPoint);
                }
            }
            this.Precipitation = newList;

            foreach (var flake in this.Precipitation)
            {
                gfx.DrawRectangle(Pens.LightGray, new Rectangle(flake, new Size(2, 2)));
                gfx.DrawRectangle(Pens.LightBlue, new Rectangle(flake, new Size(1, 1)));
            }
        }

        private void DrawSnow(Graphics gfx, int arenaSize)
        {
            Bitmap snow = UIManager.GetBitmapResource("Snow");
            Bitmap snowREnd = UIManager.GetBitmapResource("SnowRightEnd");
            Bitmap snowLEnd = UIManager.GetBitmapResource("SnowLeftEnd");
            Bitmap paintSnow;
            Bitmap arenaFloorTile = UIManager.GetBitmapResource("ArenaFloor");            
            var width = snow.Width;
            int leftpos = ((panelDrawArea.Width / 2) - ((arenaFloorTile.Width * arenaSize) / 2));

            for (int i = 0; i < arenaSize; i++)
            {
                if(i == 0)
                {
                    paintSnow = snowLEnd;
                }
                else if (i == arenaSize - 1)
                {
                    paintSnow = snowREnd;
                    paintSnow.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                }
                else
                {
                    paintSnow = snow;
                }

                paintSnow.MakeTransparent(Color.Black);
                gfx.DrawImage(paintSnow, new Point(leftpos + (i * width), panelDrawArea.Height - snow.Height - arenaFloorTile.Height + 10));
            }
        }

        public void DrawArenaFloor(Graphics gfx, int arenaSize)
        {
            _arenaPositions = new int[arenaSize];
            Bitmap arenaFloorTile = UIManager.GetBitmapResource("ArenaFloor");
            int leftpos = ((panelDrawArea.Width / 2) - ((arenaFloorTile.Width * arenaSize) / 2));

            int arraypos = 0;
            for (var x = leftpos; x < (arenaFloorTile.Width * arenaSize) + leftpos; x = x + (arenaFloorTile.Width))
            {
                _arenaPositions[arraypos] = x;
                arraypos++;
                gfx.DrawImage(arenaFloorTile, new Point(x, panelDrawArea.Height - arenaFloorTile.Height));
            }
        }

        public void DrawWater(Graphics gfx, int frame)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var name = $"Water_{1 + (frame % 2)}";
            Bitmap water = UIManager.GetBitmapResource(name);
            water.MakeTransparent(Color.White);
            var width = water.Width;
            for(int i = 0; i < panelDrawArea.Width;  i += width)
            {                
                gfx.DrawImage(water, new Point(i, panelDrawArea.Height - water.Height + random.Next(0, 5)));
            }            
        }

        public void DrawLeftBot(Graphics gfx, int arenaWidth, BotMove bot, int frame, Options options)
        {
            DrawBot(gfx, arenaWidth, bot, frame, Direction.Left,  -450, options);
        }

        public void WriteReallyBigText(string text)
        {
            using (var g = panelDrawArea.CreateGraphics())
            {
                var stringFormat = new StringFormat();
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;

                g.DrawString(text,
                new Font(Font.FontFamily, 48, FontStyle.Bold),
                Brushes.Black,
                new Rectangle(0, 0, panelDrawArea.Width, panelDrawArea.Height),
                stringFormat);                
            }
        }

        public void DrawRightBot(Graphics gfx, int arenaWidth, BotMove bot, int frame, Options options)
        {
            DrawBot(gfx, arenaWidth, bot, frame, Direction.Right, 200, options);
        }

        public void DrawBot(Graphics gfx, int arenaWidth, BotMove bot, int frame, Direction direction, int bubbleXOffset, Options options)
        {
            var image = GetImageName(direction, bot.Move, frame);
            Bitmap botImage = UIManager.GetBitmapResource(image);
            botImage.MakeTransparent(Color.White);

            Bitmap arenaFloorTile = UIManager.GetBitmapResource("ArenaFloor");

            int yDeadDrop = 0;
            int arenaPosition;
            if (bot.Bot.Position < 0 || bot.Bot.Position >= arenaWidth)
            {
                yDeadDrop = 20 - botImage.Height;
                if (bot.Bot.Position < 0)
                {
                    arenaPosition = _arenaPositions[0] - 100;
                }
                else
                {
                    arenaPosition = _arenaPositions[arenaWidth - 1] + 100;
                }
            }
            else
            {
                arenaPosition = _arenaPositions[bot.Bot.Position];
            }
            
            if (bot != null)
            {

                if (bot.Bot.IsFlipped)
                {
                    var roFlip = direction == Direction.Right
                        ? RotateFlipType.Rotate90FlipNone
                        : RotateFlipType.Rotate270FlipNone;

                    int botxpos = direction == Direction.Right
                    ? arenaPosition
                    : arenaPosition - botImage.Height;

                    botImage.RotateFlip(roFlip);

                    gfx.DrawImage(botImage,
                        new Point(botxpos, panelDrawArea.Height - botImage.Height - 55 - yDeadDrop));
                }
                else if (bot.Bot.Health <= 0)
                {
                    var roFlip = direction == Direction.Right
                        ? RotateFlipType.Rotate270FlipNone
                        : RotateFlipType.Rotate90FlipNone;                    

                    botImage.RotateFlip(roFlip);

                    gfx.DrawImage(botImage,
                        new Point(arenaPosition, panelDrawArea.Height - (arenaFloorTile.Height * 2)));
                }
                else
                {
                    int botxpos = direction == Direction.Right
                    ? arenaPosition - (botImage.Width - 70) :
                    arenaPosition;

                    gfx.DrawImage(botImage, new Point(botxpos, panelDrawArea.Height - botImage.Height - 55 - yDeadDrop));
                }
            }

            var bubbleName = GetBubbleName(bot.Move);
            if (bubbleName != null)
            {
                Bitmap bubble = UIManager.GetBitmapResource(bubbleName);
                bubble.MakeTransparent(Color.White);
                if (bubble != null)
                {
                    var bubblexpos = direction == Direction.Right
                        ? panelDrawArea.Width - (bubble.Width + 20)
                        : 20;

                    gfx.DrawImage(bubble, new Point(bubblexpos, panelDrawArea.Height - (bubble.Height + 55)));
                }
            }

            DrawHealthbar(gfx, bot, direction, options, frame);
        }

        private void DrawHealthbar(Graphics gfx, BotMove bot, Direction direction, Options options, int frame)
        {
            var xOffset = direction == Direction.Left
                ? 10
                : panelDrawArea.Width - 400;

            gfx.DrawString(bot.Bot.Name,
                new Font(Font.FontFamily, 24, FontStyle.Bold),
                Brushes.Black,
                new PointF(xOffset, 10));

            DrawBar(gfx, xOffset, 50, Brushes.Green, Brushes.Red, bot.Bot.StartingHealth, bot.Bot.Health);
            DrawBar(gfx, xOffset, 70, Brushes.LightBlue, Brushes.Black, bot.Bot.StartingNumberOfFlips, bot.Bot.NumberOfFlipsRemaining);
            DrawBar(gfx, xOffset, 90, Brushes.Yellow, Brushes.Black, bot.Bot.StartingFlameThrowerFuel, bot.Bot.FlameThrowerFuelRemaining);

            if(options.IsChristmas && direction == Direction.Left)
            {
                var random = new Random();
                if(!WhereIsSanta.HasValue)
                {
                    WhatIsSantaUpTo = 0;
                    WhereIsSanta = 300;
                }

                // Does he start walking?
                if(WhatIsSantaUpTo == 0 && random.Next(0, 100) > 80)
                {
                    WhatIsSantaUpTo = random.Next(1, 2);
                }

                // Does he stop walking?
                if (WhatIsSantaUpTo != 0 && random.Next(0, 100) > 95)
                {
                    WhatIsSantaUpTo = 0;
                }

                // is he about to walk off the edge (or just fancies a new direction)
                if (random.Next(0, 100) > 95 || WhereIsSanta.Value < 100 || WhereIsSanta > 350)
                {
                    if (WhatIsSantaUpTo == 2) WhatIsSantaUpTo = 1;
                    else if (WhatIsSantaUpTo == 1) WhatIsSantaUpTo = 2;
                }
                if (WhatIsSantaUpTo == 2) WhereIsSanta += 2;
                if (WhatIsSantaUpTo == 1) WhereIsSanta -= 2;


                // We probably don't need to do this every frame!
                var allSantas = UIManager.GetBitmapResource("Santa");
                var frames = new List<List<Bitmap>>();
                var width = allSantas.Width / 3;
                var height = allSantas.Height / 4;
                for (int row = 0; row < 4; row++)
                {
                    frames.Add(new List<Bitmap>()); // there are four rows
                    for (int column = 0; column < 3; column++)
                    {
                        frames[row].Add(allSantas.Clone(new Rectangle(column * width, row * height, width, height), allSantas.PixelFormat));
                        frames[row][column].MakeTransparent(Color.White);
                    }
                }

                var frameToDraw = WhatIsSantaUpTo == 0 ? 1 : frame - 1;
                gfx.DrawImage(frames[WhatIsSantaUpTo.Value][frameToDraw], new Point(WhereIsSanta.Value, 50 - frames[0][0].Height));
            }
        }

        protected int? WhatIsSantaUpTo { get; set; }
        protected int? WhereIsSanta { get; set; }

        private void DrawBar(Graphics gfx, int xOffset, int yOffset, Brush fullColour, Brush usedColour, int full, int remaining)
        {
            var percentRemaining = (100d * remaining) / full;

            gfx.FillRectangle(usedColour, new RectangleF(xOffset, yOffset, 400, 20));
            gfx.FillRectangle(fullColour, new RectangleF(xOffset, yOffset, (float)(4 * percentRemaining), 20));
        }

        public string GetImageName(Direction direction, Move move, int frame)
        {
            var actionName = GetActionName(move);
            var directionStr = direction == Direction.Left ? "left" : "right";
            var frameAdj = Math.Min(frame, MaxNumberOfFrames(move));
            return $"{actionName}_{directionStr}_{frameAdj}";
        }

        private int MaxNumberOfFrames(Move move)
        {
            switch(move)
            {
                case Logic.Move.AttackWithAxe:
                case Logic.Move.Shunt:
                case Logic.Move.Flip:
                case Logic.Move.FlameThrower:
                    return 3;
                case Logic.Move.MoveForwards:
                    return 2;
                default:
                    return 1;
            }
        }

        private string GetActionName(Move move)
        {
            switch(move)
            {
                case Logic.Move.AttackWithAxe:
                    return "Axe";
                case Logic.Move.FlameThrower:
                    return "Flame";
                case Logic.Move.Shunt:
                    return "Shunt";
                case Logic.Move.Flip:
                    return "Flip";
                default:
                    return "Neutral";
            }
        }

        private string GetBubbleName(Move move)
        {
            switch (move)
            {
                case Logic.Move.AttackWithAxe:
                    return "Axe";
                case Logic.Move.FlameThrower:
                    return "Flame";
                case Logic.Move.Shunt:
                    return "Shunt";
                case Logic.Move.Flip:
                    return "Flip";
                case Logic.Move.MoveForwards:
                    return "Forwards";
                default:
                    return null;
            }
        }
        
        public void DrawImageOnUIPanel(Bitmap image, Point location )
        {
            

            //ColorMatrix matrix = new ColorMatrix();
            //matrix.Matrix33 = 0.1f;
            //ImageAttributes attributes = new ImageAttributes();
            //attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            Bitmap destImg = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(destImg);

            g.DrawImage((Image) image, new Rectangle(0, 0, destImg.Width, destImg.Height),
                 0, 0, image.Width, image.Height,GraphicsUnit.Pixel);

          

            g = panelDrawArea.CreateGraphics();           
            g.DrawImage(destImg, location);            
        }

}
}
