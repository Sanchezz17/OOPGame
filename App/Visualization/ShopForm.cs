using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain.Units;

namespace App.Visualization
{
    public class ShopForm : DBForm
    {
        private readonly GameForm gameForm;
        private ListBox heroesList;
        private ListBox heroParametersList;
        private Label heroLabel;
        private Button upgradeButton;
        private Dictionary<string, IDescribe> describeObjects;
        private IDescribe currentDescribeObject;
        private HashSet<Characteristic> currentParameters;
        private Characteristic currentParameter;
        private Label coinsLabel;
        public ShopForm(GameForm gameForm)
        {
            describeObjects = new Dictionary<string, IDescribe>();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                                                | ControlStyles.UserPaint, true);
            UpdateStyles();
            this.gameForm = gameForm;
            Text = "Shop";
            Size = gameForm.Size;
            Location = gameForm.Location;
            VisibleChanged += OnVisibleChanged;
            InitializeShopForm();
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (Visible && Owner != null)
            {
                Location = Owner.Location;
                Size = Owner.Size;
            }
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
            FormUtils.SplitRowsByPercentages(leftPanel.RowStyles, new []{10F, 90F});

            var label = FormUtils.GetLabelWithTextAndFontColor("Герои", Color.Blue, 30);
            leftPanel.Controls.Add(label);

            heroesList = new ListBox();
            heroesList.Items.AddRange(
                gameForm.Game.Player.Heroes
                    .Select(hero => hero.Type.Name)
                    .Cast<object>()
                    .ToArray());
            describeObjects = gameForm.Game.Player.Heroes
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
            if (heroesList.SelectedItem != null)
            {
                currentDescribeObject = describeObjects[heroesList.SelectedItem.ToString()];
                var visualObject = gameForm.ResourceManager.GetVisualObject(currentDescribeObject.Type);
                heroLabel.BackgroundImage = visualObject.PassiveImage;
                heroLabel.BackgroundImageLayout = ImageLayout.Zoom;
            }
            UpdateParametersList();
        }

        private void UpdateParametersList()
        {
            heroParametersList.Items.Clear();
            if (currentDescribeObject != null)
            {
                currentParameters = currentDescribeObject.Parameters.Characteristics;
                heroParametersList.Items.AddRange(
                    currentParameters
                        .Select((characteristic) => $"{characteristic.Name}: {characteristic.Value}")
                        .Cast<object>()
                        .ToArray());
            }
        }

        private TableLayoutPanel GetCenterPanel()
        {
            var centerPanel = FormUtils.GetTableLayoutPanel(3, 1);
            FormUtils.SplitRowsByPercentages(
                centerPanel.RowStyles, new float[]{40F, 50F, 10F});

            heroLabel = FormUtils.GetTransparentLabel();
            centerPanel.Controls.Add(heroLabel, 0, 0);

            heroParametersList = new ListBox
            {
                ForeColor = Color.Black,
                Font = new Font("Arial", 30)
            };
            FormUtils.SetAnchorForAllSides(heroParametersList);
            heroParametersList.SelectedIndexChanged += SelectedParameterChanged;
            centerPanel.Controls.Add(heroParametersList, 0, 1);

            upgradeButton = FormUtils.GetButtonWithTextAndFontColor(
                "Upgrade", Color.Red, 30);
            centerPanel.Controls.Add(upgradeButton, 0, 2);
            upgradeButton.Click += UpgradeParameter;

            return centerPanel;
        }

        public void UpdateCoinsLabel()
        {
            coinsLabel.Text = gameForm.Game.Player.Coins.ToString();
        }

        private void UpgradeParameter(object sender, EventArgs e)
        {
            if (currentDescribeObject != null &&
                currentParameter != null)
            {
                if (gameForm.Game.Player.Coins >= currentParameter.UpgradePrice)
                {
                    gameForm.Game.Player.Coins -= currentParameter.UpgradePrice;
                    UpdateCoinsLabel();
                    var upgradeValue = (int) (currentParameter.Value * 0.1);
                    if (currentParameter.Name == "Reload")
                        upgradeValue *= -1;
                    currentParameter.Upgrade(upgradeValue);
                    UpdateParametersList();
                    OnUpgradePriceChange();
                }
                else
                {
                    MessageBox.Show("Недостаточно монет");
                }
            }
        }
        
        private void SelectedParameterChanged(object sender, EventArgs e)
        {
            if (heroParametersList.SelectedItem != null)
            {
                var parameterName = heroParametersList.SelectedItem.ToString().Split(':')[0];
                currentParameter =
                    currentParameters
                        .First(p => p.Name == parameterName);
                OnUpgradePriceChange();
            }
        }

        private void OnUpgradePriceChange()
        {
            upgradeButton.Text = $"Upgrade: {currentParameter.UpgradePrice}";
        }

        private TableLayoutPanel GetRightPanel()
        {
            var rightPanel = FormUtils.GetTableLayoutPanel(4, 1);

            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor(
                "В главное меню", Color.Blue, 20);
            exitToMenuButton.Click += SwitchToMenu;
            rightPanel.Controls.Add(exitToMenuButton, 0, 0);

            var coinsTable = FormUtils.GetTableLayoutPanel(2, 1);
            FormUtils.SplitRowsByPercentages(coinsTable.RowStyles, new float[]{80F, 20F});
            rightPanel.Controls.Add(coinsTable, 0, 3);

            var coinsImage = FormUtils.GetLabelWithTextAndFontColor();
            coinsImage.BackgroundImage = gameForm.ResourceManager.GetVisualObject("Coins").PassiveImage;
            coinsImage.BackgroundImageLayout = ImageLayout.Zoom;
            coinsImage.BorderStyle = BorderStyle.None;
            coinsTable.Controls.Add(coinsImage, 0, 0);

            coinsLabel = FormUtils.GetLabelWithTextAndFontColor(
                gameForm.Game.Player.Coins.ToString(), Color.Black, 15);
            coinsLabel.BorderStyle = BorderStyle.None;
            coinsTable.Controls.Add(coinsLabel, 0, 1);
            return rightPanel;
        }

        private void SwitchToMenu(object sender, EventArgs e)
        {
            Hide();
            gameForm.MainMenuForm.Show(this);
        }
    }
}