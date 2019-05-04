using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP_Game.GameLogic;

namespace OOP_Game
{
    public partial class GameWindow : Form
    {
        private const int RowsCount = 5;
        private const int ColumnsCount = 9;
        
        private Game Game = GameFactory.GetStandardGame();
        private Form mainMenu;
        private TableLayoutPanel fieldPanel;
        private Label currentLevelLabel;
        private Label scoreLabel;
        private TableLayoutPanel purchasePanel;
        private readonly Image gemImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\gem.jpg");
        
        public GameWindow()
        {
            DoubleBuffered = true;
            Name = "GameForm";
            Text = "OOPGame";
            Paint += OnPaint;
            Size = new Size(1200, 700);
            mainMenu = new MainMenu(this);
            Shown += SwitchToMenu;
            InitializeGameWindow();
        }

        private void InitializeGameWindow()
        {
            var mainPanel = FormUtils.InitializeTableLayoutPanel(2, 1);
            mainPanel.Size = new Size(Size.Width - 15, Size.Height - 40);
            mainPanel.Location = Location;
            mainPanel.RowStyles[0] = new RowStyle(SizeType.Percent, 25F);
            mainPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 75F);

            var topPanel = FormUtils.InitializeTableLayoutPanel(1, 4);
            topPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 10F);
            topPanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 10F);
            topPanel.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 60F);
            topPanel.ColumnStyles[3] = new ColumnStyle(SizeType.Percent, 20F);

            currentLevelLabel = FormUtils.GetLabelWithTextAndFontColor(
                "Уровень " + (Game.CurrentLevelNumber + 1).ToString(),
                Color.Black, 15);
            topPanel.Controls.Add(currentLevelLabel, 0, 0);

            scoreLabel = FormUtils.GetLabelWithTextAndFontColor("", Color.Black, 15);
            scoreLabel.BackgroundImage = gemImage;
            scoreLabel.BackgroundImageLayout = ImageLayout.Zoom;
            scoreLabel.TextAlign = ContentAlignment.BottomCenter;
            scoreLabel.Text = Game.CurrentLevel.Score.ToString();
            topPanel.Controls.Add(scoreLabel, 1, 0);

            purchasePanel = FormUtils.InitializeTableLayoutPanel(1, 5);
            
            // фейковый Железный Человек на панели
            var ironManPurchase = FormUtils.GetButtonWithTextAndFontColor("50", Color.Black, 15);
            ironManPurchase.BackgroundImage = Image.FromFile(
                Environment.CurrentDirectory + @"\Resources\Heroes\IronMan\static.gif");
            ironManPurchase.BackgroundImageLayout = ImageLayout.Zoom;
            ironManPurchase.TextAlign = ContentAlignment.BottomCenter;
            ironManPurchase.Margin = Padding.Empty;
            // добавить персонажей, доступных к покупке на панель
            purchasePanel.Controls.Add(ironManPurchase, 0, 0);
            
            topPanel.Controls.Add(purchasePanel, 2, 0);
            
            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor("Главное меню", Color.Blue, 15);
            exitToMenuButton.Margin = Padding.Empty;
            exitToMenuButton.Click += SwitchToMenu;
            topPanel.Controls.Add(exitToMenuButton, 3, 0);            
            mainPanel.Controls.Add(topPanel, 0, 0);

            var gamePanel = GetGamePanel();
            mainPanel.Controls.Add(gamePanel, 0, 1);           
            Controls.Add(mainPanel);
        }

        private TableLayoutPanel GetGamePanel()
        {
            var gamePanel = FormUtils.InitializeTableLayoutPanel(1, 2);
            gamePanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
            gamePanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80F);

            var headquartersControl = new Label();
            headquartersControl.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\headquarters.jpg");
            headquartersControl.BackgroundImageLayout = ImageLayout.Stretch;
            headquartersControl.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                                          AnchorStyles.Top | AnchorStyles.Bottom);
            headquartersControl.Margin = Padding.Empty;
            gamePanel.Controls.Add(headquartersControl, 0, 0);

            fieldPanel = FormUtils.InitializeTableLayoutPanel(5, 9);
            fieldPanel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\field.jpg");
            fieldPanel.BackgroundImageLayout = ImageLayout.Stretch;
            fieldPanel.Margin = Padding.Empty;
            // Именно внутри fieldControl и будут происходить основные события игры
            
            //фейковый Железный человек на поле
//            var ironManFake = new Label();
//            ironManFake.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
//                      AnchorStyles.Top | AnchorStyles.Bottom);
//            ironManFake.BackgroundImage = Image.FromFile(
//                Environment.CurrentDirectory + @"\Resources\Heroes\IronMan\static.gif");
//            ironManFake.BackgroundImageLayout = ImageLayout.Stretch;
//            ironManFake.BackColor = Color.Transparent;
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var ironManFake = new Label();
                    ironManFake.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                                          AnchorStyles.Top | AnchorStyles.Bottom);
                    ironManFake.BackgroundImage = Image.FromFile(
                        Environment.CurrentDirectory + @"\Resources\Heroes\IronMan\static.gif");
                    ironManFake.BackgroundImageLayout = ImageLayout.Stretch;
                    ironManFake.BackColor = Color.Transparent;
                    fieldPanel.Controls.Add(ironManFake, j, i);
                }
            }
            //fieldPanel.Controls.Add(ironManFake, 3, 3);
            
            
            gamePanel.Controls.Add(fieldPanel, 1, 0);
            return gamePanel;
        }

        private void AddHeroToField(object sender, EventArgs e)
        {
            
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
    }
}
