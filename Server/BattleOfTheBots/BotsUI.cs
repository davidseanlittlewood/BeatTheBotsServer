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

namespace BattleOfTheBots.UIControl
{
    public partial class BotsUI : UserControl
    {
        public BotsUI()
        {
            InitializeComponent();
        }

        int[] _arenaPositions;        

        public void Update(int arenaWidth, BotMove leftBot, BotMove rightBot, int frame)
        {
            var bitmap = new Bitmap(panelDrawArea.Width, panelDrawArea.Height);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.FillRectangle(Brushes.White, 0, 0, panelDrawArea.Width, panelDrawArea.Height);
                this.DrawArenaFloor(gfx, arenaWidth);
                this.DrawLeftBot(gfx, arenaWidth, leftBot, frame);
                this.DrawRightBot(gfx, arenaWidth, rightBot, frame);
                this.DrawWater(gfx);
            }
            this.DrawImageOnUIPanel(bitmap, new Point(0, 0));
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

        public void DrawWater(Graphics gfx)
        {
            Bitmap water = UIManager.GetBitmapResource("Water");
            water.MakeTransparent(Color.White);
            var width = water.Width;
            for(int i = 0; i < panelDrawArea.Width;  i += width)
            {                
                gfx.DrawImage(water, new Point(i, panelDrawArea.Height - water.Height));
            }            
        }

        public void DrawLeftBot(Graphics gfx, int arenaWidth, BotMove bot, int frame)
        {
            DrawBot(gfx, arenaWidth, bot, frame, Direction.Left,  -450);            
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

        public void DrawRightBot(Graphics gfx, int arenaWidth, BotMove bot, int frame)
        {
            DrawBot(gfx, arenaWidth, bot, frame, Direction.Right, 200);
        }

        public void DrawBot(Graphics gfx, int arenaWidth, BotMove bot, int frame, Direction direction, int bubbleXOffset)
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

            DrawHealthbar(gfx, bot, direction);
        }

        private void DrawHealthbar(Graphics gfx, BotMove bot, Direction direction)
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
        }

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
