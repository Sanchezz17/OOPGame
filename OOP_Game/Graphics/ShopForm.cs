using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OOP_Game.Infrastructure;

namespace OOP_Game
{
    public class ShopForm : Form
    {
        private GameWindow gameWindow;
        private ListBox heroesList;
        private ListBox heroParametersList;
        private Label heroLabel;
        private Dictionary<string, DescribeObject> describeObjects;
        public ShopForm(GameWindow gameWindow)
        {
            describeObjects = new Dictionary<string, DescribeObject>();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                                                | ControlStyles.UserPaint, true);
            UpdateStyles();
            this.gameWindow = gameWindow;
            Text = "Shop";
            Size = gameWindow.Size;
            Location = gameWindow.Location;
            InitializeShopForm();
        }

        private void InitializeShopForm()
        {
            var mainPanel = FormUtils.GetTableLayoutPanel(1, 3);
            mainPanel.Size = new Size(Size.Width - 15, Size.Height - 40);
            FormUtils.SplitColumnsByPercentages(
                mainPanel.ColumnStyles, new float[] {40F, 40F, 20F});
            Controls.Add(mainPanel);

            var leftPanel = GetLeftPanel();
            mainPanel.Controls.Add(leftPanel, 0, 0);

            var centerPanel = GetCenterPanel();
            mainPanel.Controls.Add(centerPanel, 1, 0);

            var rightPanel = GetRightPanel();
            mainPanel.Controls.Add(rightPanel, 2, 0);
        }

        private TableLayoutPanel GetLeftPanel()
        {
            var leftPanel = FormUtils.GetTableLayoutPanel(2, 1);
            FormUtils.SplitRowsByPercentages(leftPanel.RowStyles, new float[]{10F, 90F});

            var label = FormUtils.GetLabelWithTextAndFontColor("Герои", Color.Blue, 30);
            leftPanel.Controls.Add(label);

            heroesList = new ListBox();
            heroesList.Items.AddRange(
                gameWindow.Game.Player.Heroes
                    .Select(hero => hero.Type.Name)
                    .Cast<object>()
                    .ToArray());
            describeObjects = gameWindow.Game.Player.Heroes
                .ToDictionary(hero => hero.Type.Name, hero => hero);
            heroesList.ForeColor = Color.Black;
            heroesList.Font = new Font("Arial", 30);
            heroesList.Margin = Padding.Empty;
            FormUtils.SetAnchorForAllSides(heroesList);
            heroesList.SelectedIndexChanged += SelectedHeroChanged;
            leftPanel.Controls.Add(heroesList, 0, 1);
            
            return leftPanel;
        }

        private void SelectedHeroChanged(object sender, EventArgs e)
        {
            var describeObject = describeObjects[heroesList.SelectedItem.ToString()];
            var visualObject = gameWindow.ResourceManager.VisualObjects[describeObject.Type.Name];
            heroLabel.BackgroundImage = visualObject.PassiveImage;
            heroLabel.BackgroundImageLayout = ImageLayout.Zoom;
        }
        
        private TableLayoutPanel GetCenterPanel()
        {
            var centerPanel = FormUtils.GetTableLayoutPanel(3, 1);
            FormUtils.SplitRowsByPercentages(
                centerPanel.RowStyles, new float[]{40F, 50F, 10F});

            heroLabel = FormUtils.GetTransparentLabel();
            centerPanel.Controls.Add(heroLabel, 0, 0);
            
            heroParametersList = new ListBox();
            FormUtils.SetAnchorForAllSides(heroParametersList);
            centerPanel.Controls.Add(heroParametersList, 0, 1);

            var upgradeButton = FormUtils.GetButtonWithTextAndFontColor(
                "Upgrade", Color.Red, 30);
            centerPanel.Controls.Add(upgradeButton, 0, 2);
            // upgradeButton.Click += событие улучшения

            return centerPanel;
        }
        
        private TableLayoutPanel GetRightPanel()
        {
            var rightPanel = FormUtils.GetTableLayoutPanel(4, 1);

            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor("В главное меню", Color.Blue, 20);
            exitToMenuButton.Click += SwitchToMenu;
            rightPanel.Controls.Add(exitToMenuButton, 0, 0);

            var coinsTable = FormUtils.GetTableLayoutPanel(2, 1);
            FormUtils.SplitRowsByPercentages(coinsTable.RowStyles, new float[]{80F, 20F});
            rightPanel.Controls.Add(coinsTable, 0, 3);

            var coinsImage = FormUtils.GetLabelWithTextAndFontColor("", Color.Transparent, 30);
            coinsImage.BackgroundImage = gameWindow.ResourceManager.VisualObjects["Coins"].PassiveImage;
            coinsImage.BackgroundImageLayout = ImageLayout.Zoom;
            coinsImage.BorderStyle = BorderStyle.None;
            coinsTable.Controls.Add(coinsImage, 0, 0);

            var coinsLabel = FormUtils.GetLabelWithTextAndFontColor(
                gameWindow.Game.Player.Coins.ToString(), Color.Black, 15);
            coinsLabel.BorderStyle = BorderStyle.None;
            coinsTable.Controls.Add(coinsLabel, 0, 1);
            return rightPanel;
        }

        private void SwitchToMenu(object sender, EventArgs e)
        {
            Hide();
            gameWindow.MainMenu.Size = Size;
            gameWindow.MainMenu.Location = Location;
            gameWindow.MainMenu.Show();
        }
    }
}