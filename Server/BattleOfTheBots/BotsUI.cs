﻿using System;
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

namespace BattleOfTheBots.UIControl
{
    public partial class BotsUI : UserControl
    {
        public BotsUI()
        {
            InitializeComponent();
        }

        int[] _arenaPositions;

        public void DrawArenaFloor(Graphics gfx, int ArenaSize)
        {
            _arenaPositions = new int[ArenaSize];
            Bitmap arenaFloorTile = UIManager.GetBitmapResource("ArenaFloor");            
            int leftpos = ((panelDrawArea.Width / 2) - ((arenaFloorTile.Width * ArenaSize) / 2));

            int arraypos = 0;
            for (var x = leftpos; x < (arenaFloorTile.Width * ArenaSize) + leftpos; x = x + (arenaFloorTile.Width))
            {
                _arenaPositions[arraypos] = x;
                arraypos++;
                gfx.DrawImage(arenaFloorTile, new Point(x, panelDrawArea.Height - arenaFloorTile.Height));
            }
        }

        public void Update(int arenaWidth, BotMove leftBot, BotMove rightBot, int frame)
        {
            var bitmap = new Bitmap(panelDrawArea.Width, panelDrawArea.Height);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.FillRectangle(Brushes.White, 0, 0, panelDrawArea.Width, panelDrawArea.Height);
                this.DrawArenaFloor(gfx, arenaWidth);
                this.DrawLeftBot(gfx, leftBot, frame);
                this.DrawRightBot(gfx, rightBot, frame);                
            }
            this.DrawImageOnUIPanel(bitmap, new Point(0, 0));
        }


        public void DrawLeftBot(Graphics gfx, BotMove bot, int frame)
        {
            DrawBot(gfx, bot, frame, Direction.Left, -450);            
        }

        public void DrawRightBot(Graphics gfx, BotMove bot, int frame)
        {
            DrawBot(gfx, bot, frame, Direction.Right, 200);
        }

        public void DrawBot(Graphics gfx, BotMove bot, int frame, Direction direction, int bubbleXOffset)
        {            
            if (bot.Bot.Position >= 0 && bot.Bot.Position < 9)
            {
                var image = GetImageName(direction, bot.Move, frame);
                Bitmap botImage = UIManager.GetBitmapResource(image);
                if (bot != null)
                {
                    if (bot.Bot.IsFlipped)
                    {
                        var roFlip = direction == Direction.Right
                            ? RotateFlipType.Rotate90FlipNone
                            : RotateFlipType.Rotate270FlipNone;

                        botImage.RotateFlip(roFlip);
                        gfx.DrawImage(botImage,
                            new Point(_arenaPositions[bot.Bot.Position] - botImage.Width, panelDrawArea.Height - botImage.Height - 17));
                    }
                    else
                    {
                        gfx.DrawImage(botImage, new Point(_arenaPositions[bot.Bot.Position] - botImage.Width, panelDrawArea.Height - botImage.Height - 17));
                    }
                }

                var bubbleName = GetBubbleName(bot.Move);
                if (bubbleName != null)
                {
                    Bitmap bubble = UIManager.GetBitmapResource(bubbleName);
                    if (bubble != null)
                    {
                        gfx.DrawImage(bubble, new Point(_arenaPositions[bot.Bot.Position] - 68 + bubbleXOffset, panelDrawArea.Height - botImage.Height - 127));
                    }
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

            var percentHealthRemaining = (100d * bot.Bot.Health) / bot.Bot.StartingHealth;

            gfx.FillRectangle(Brushes.Red, new RectangleF(xOffset, 50, 400, 20));
            gfx.FillRectangle(Brushes.Green, new RectangleF(xOffset, 50, (float)(4 * percentHealthRemaining), 20));
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
                    return 3;
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
                default:
                    return null;
            }
        }

        public void DrawImageOnUIPanel(Bitmap image, Point location)
        {
            Graphics g = panelDrawArea.CreateGraphics();
            g.DrawImage(image, location);
        }
    }
}
