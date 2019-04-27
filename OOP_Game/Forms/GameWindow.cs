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
        private readonly Size SquareSize = new Size(80, 80);
        private const int RowsCount = 5;
        private const int ColumnsCount = 9;
        
        private Form mainMenu;
        
        public GameWindow()
        {
            Name = "GameForm";
            Text = "OOPGame";
            Paint += OnPaint;
            Size = new Size(SquareSize.Width * (ColumnsCount + 2),
                SquareSize.Height * (RowsCount + 2));
            mainMenu = new MainMenu(this);
            Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            Hide();
            mainMenu.Location = Location;
            mainMenu.Size = Size;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawMap(sender, e);
            e.Graphics.DrawString("Игра в процессе разработки...",
                new Font("Arial", 20),
                Brushes.Black,
                new PointF(Location.X, Location.Y + 340));
        }

        private void DrawMap(object sender, PaintEventArgs e)
        {
            for (var row = 0; row < RowsCount; row++)
            {
                for (var column = 0; column < ColumnsCount; column++)
                {
                    e.Graphics.DrawRectangle(
                        new Pen(Brushes.Blue), 
                        new Rectangle(new Point(column * SquareSize.Width, row * SquareSize.Height),
                            SquareSize));
                }
            }
        }
    }
}
