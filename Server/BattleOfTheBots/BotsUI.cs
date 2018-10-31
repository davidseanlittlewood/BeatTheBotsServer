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

        public void DrawLeftBot(int Position)
        {
            Bitmap leftBot1 = UIManager.GetBitmapResource("Neutral_left_1");
            DrawImageOnUIPanel(leftBot1, new Point(_arenaPositions[Position] - 68, panelDrawArea.Height - leftBot1.Height - 17));
        }

        public void DrawRightBot(int Position)
        {
            Bitmap rightBot1 = UIManager.GetBitmapResource("Neutral_right_1");
            DrawImageOnUIPanel(rightBot1, new Point(_arenaPositions[Position] - rightBot1.Width, panelDrawArea.Height - rightBot1.Height - 17));
        }

        public void DrawImageOnUIPanel(Bitmap image, Point location)
        {
            Graphics g = panelDrawArea.CreateGraphics();
            g.DrawImage(image, location);
        }
    }
}
