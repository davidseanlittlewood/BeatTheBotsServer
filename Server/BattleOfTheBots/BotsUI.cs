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

namespace BattleOfTheBots.UIControl
{
    public partial class BotsUI : UserControl
    {
        public BotsUI()
        {
            InitializeComponent();
        }

        int[] _arenaPositions;

        public void DrawArenaFloor(int ArenaSize)
        {
            _arenaPositions = new int[ArenaSize];
            Bitmap arenaFloorTile = UIManager.GetBitmapResource("ArenaFloor");            
            int leftpos = ((panelDrawArea.Width / 2) - ((arenaFloorTile.Width * ArenaSize) / 2));

            int arraypos = 0;
            for (var x = leftpos; x < (arenaFloorTile.Width * ArenaSize) + leftpos; x = x + (arenaFloorTile.Width))
            {
                _arenaPositions[arraypos] = x;
                arraypos++;
                DrawImageOnUIPanel(arenaFloorTile, new Point(x, panelDrawArea.Height - arenaFloorTile.Height));
            }
        }

        public void DrawLeftBot(int position, Move move, int frame)
        {
            if (position >= 0 && position < 9)
            {
                var image = GetImageName(Direction.Left, move, frame);
                Bitmap leftBot1 = UIManager.GetBitmapResource(image);
                if (leftBot1 != null)
                {
                    DrawImageOnUIPanel(leftBot1, new Point(_arenaPositions[position] - 68, panelDrawArea.Height - leftBot1.Height - 17));
                }
            }
        }
        public void DrawRightBot(int position, Move move, int frame)
        {
            if (position >= 0 && position < 9)
            {
                var image = GetImageName(Direction.Right, move, frame);
                Bitmap rightBot1 = UIManager.GetBitmapResource(image);
                if (rightBot1 != null)
                {
                    DrawImageOnUIPanel(rightBot1, new Point(_arenaPositions[position] - rightBot1.Width, panelDrawArea.Height - rightBot1.Height - 17));
                }
            }
        }

        public void Clear()
        {
            var bitmap = new Bitmap(panelDrawArea.Width, panelDrawArea.Height);
            using (var gfx = Graphics.FromImage(bitmap))
            {

                gfx.FillRectangle(Brushes.White, 0, 0, panelDrawArea.Width, panelDrawArea.Height);
            }
            this.DrawImageOnUIPanel(bitmap, new Point(0, 0));
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
                default:
                    return "Neutral";
            }
        }

        public void DrawImageOnUIPanel(Bitmap image, Point location)
        {
            Graphics g = panelDrawArea.CreateGraphics();
            g.DrawImage(image, location);
        }
    }
}
