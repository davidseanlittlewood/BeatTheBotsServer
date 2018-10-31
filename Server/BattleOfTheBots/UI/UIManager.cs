using BattleOfTheBots.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheBots.UI
{
    public static class UIManager
    {
        public static Bitmap DrawBot(Bot bot)
        {
            var bitmap = new Bitmap(100, 100);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                using (SolidBrush myBrush = new SolidBrush(Color.Red))
                {
                    gfx.FillRectangle(myBrush, new Rectangle(15, 60, 70, 30));                    
                }

                using (SolidBrush myBrush = new SolidBrush(Color.Black))
                {
                    gfx.FillEllipse(myBrush, new Rectangle(20, 80, 20, 20));
                    gfx.FillEllipse(myBrush, new Rectangle(60, 80, 20, 20));
                }
            }

            return bitmap;
        }


        public static Bitmap GetBitmapResource(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string strBaseName = assembly.GetName().Name + ".Properties.Resources";
            ResourceManager rm = new ResourceManager(strBaseName, assembly);

            return (Bitmap)rm.GetObject(name);
        }


        public static Bitmap DrawArena(Arena arena)
        {
            var bitmap = new Bitmap(200 + (100 * arena.NumberOfSquares), 500);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                using (SolidBrush myBrush = new SolidBrush(Color.Brown))
                {
                    for (int i = 1; i <= arena.NumberOfSquares; i++)
                    {
                        gfx.FillRectangle(myBrush, new Rectangle(i * 100, 400, 98, 10));
                    }
                }
            }

            return bitmap;
        }
    }
}
