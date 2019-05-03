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
//            Size = new Size(SquareSize.Width * (ColumnsCount + 2),
//                SquareSize.Height * (RowsCount + 2));
            Size = new Size(1200, 700);
            mainMenu = new MainMenu(this);
            Shown += SwitchToMenu;
            InitializeGameWindow();
        }

        private void InitializeGameWindow()
        {
            var mainPanel = FormUtils.InitializeTableLayoutPanel(2, 1);
            mainPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 25F);
            mainPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 75F);

            var buttonsPanel = FormUtils.InitializeTableLayoutPanel(1, 4);
            buttonsPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 10F);
            buttonsPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 10F);
            buttonsPanel.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 70F);
            buttonsPanel.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 20F);
            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor("Главное меню", Color.Blue, 15);
            exitToMenuButton.Margin = Padding.Empty;
            exitToMenuButton.Click += SwitchToMenu;
            buttonsPanel.Controls.Add(exitToMenuButton, 3, 0);
            
            mainPanel.Controls.Add(buttonsPanel, 0, 0);
            mainPanel.Size = new Size(Size.Width - 15, Size.Height - 40);
            mainPanel.Location = Location;

            var gamePanel = FormUtils.InitializeTableLayoutPanel(1, 2);
            gamePanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
            gamePanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80F);

            var headquartersControl = new Label();;
            headquartersControl.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\headquarters.jpg");
            headquartersControl.BackgroundImageLayout = ImageLayout.Stretch;
            headquartersControl.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                                   AnchorStyles.Top | AnchorStyles.Bottom);
            headquartersControl.Margin = Padding.Empty;
            gamePanel.Controls.Add(headquartersControl, 0, 0);

            var fieldControl = FormUtils.InitializeTableLayoutPanel(5, 9);
            fieldControl.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\field.jpg");
            fieldControl.BackgroundImageLayout = ImageLayout.Stretch;
            fieldControl.Margin = Padding.Empty;
            // Именно внутри fieldControl и будут происходить основные события игры
            gamePanel.Controls.Add(fieldControl, 1, 0);
            
            mainPanel.Controls.Add(gamePanel, 0, 1);           
            Controls.Add(mainPanel);
        }
        
        private void SwitchToMenu(object sender, EventArgs e)
        {
            Hide();
            mainMenu.Location = Location;
            mainMenu.Size = Size;
            mainMenu.Show();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //DrawMap(sender, e);
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
