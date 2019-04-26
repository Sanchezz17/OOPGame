using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Game
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            Text = "OOPGame";
            Paint += OnPaint;
            Show();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawMap(sender, e);
        }

        private void DrawMap(object sender, PaintEventArgs e)
        {
            var size = 50;
            for (var row = 0; row < 5; row++)
            {
                for (var column = 0; column < 10; column++)
                {
                    e.Graphics.DrawRectangle(
                        new Pen(Brushes.Blue), 
                        new Rectangle(column * size, row * size, size, size));
                }
            }
        }
    }
}
