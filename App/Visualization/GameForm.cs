﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Domain.GameLogic;
using Domain.Infrastructure;
using Domain.Units;
using OOP_Game.GameLogic;
using ContentAlignment = System.Drawing.ContentAlignment;
using Size = System.Drawing.Size;

namespace App.Visualization
{
    public partial class GameForm : DBForm
    {
        public Game Game { get; private set; }
        private readonly IGameFactory gameFactory;
        public ResourceManager ResourceManager { get; private set; }
        public readonly Form MainMenuForm;
        public readonly ShopForm ShopForm;
        
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel gamePanel;
        private Label field;
        private Size cellSize;        
        private TableLayoutPanel gameOverPanel;
        private TableLayoutPanel levelWinPanel;
        private TableLayoutPanel gameWinPanel;
        private Label currentLevelLabel;
        private Label scoreLabel;
        private TableLayoutPanel purchasePanel;
        private IDescribe currentObjectToPurchase;
        private Button currentPurchaseButton;
        private bool isDeleteSelected;
        private readonly AudioPlayer soundtrackPlayer;
        private readonly Player player;
        private readonly Timer timer;
        
        public GameForm(IGameFactory gameFactory, ResourceManager resourceManager, Player player)
        {
            this.gameFactory = gameFactory;
            Game = this.gameFactory.Create(player);
            ResourceManager = resourceManager;
            this.player = player;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                      | ControlStyles.UserPaint, true);
            UpdateStyles();
            Size = new Size(950, 700);
            Text = "PvZ Marvel Edition";
            MainMenuForm = new MainMenuForm(this);
            ShopForm = new ShopForm(this);
            soundtrackPlayer = new AudioPlayer(
                Environment.CurrentDirectory + @"\Resources\Music\soundtrack.mp3");
            Shown += SwitchToMenu;
            VisibleChanged += OnVisibleChanged;
            InitializeGameWindow();
            timer = new Timer {Interval = 40};
            timer.Tick += OnTimer;
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                soundtrackPlayer.Play();
                timer.Start();
            }
            else
            {
                soundtrackPlayer.Stop();
                timer.Stop();
            }
        }

        private void InitializeGameWindow()
        {
            mainPanel = FormUtils.GetTableLayoutPanel(2, 1);
            mainPanel.Size = new Size(Size.Width - 15, Size.Height - 40);
            mainPanel.Location = Location;
            FormUtils.SplitRowsByPercentages(mainPanel.RowStyles,
                new [] {25F, 75F});

            var topPanel = FormUtils.GetTableLayoutPanel(1, 5);
            FormUtils.SplitColumnsByPercentages(
                topPanel.ColumnStyles, new [] {10F, 10F, 60F, 10F, 10F});

            currentLevelLabel = FormUtils.GetLabelWithTextAndFontColor(
                "Уровень " + (Game.CurrentLevelNumber + 1),
                Color.Black, 13);
            topPanel.Controls.Add(currentLevelLabel, 0, 0);

            scoreLabel = FormUtils.GetLabelWithTextAndFontColor("", Color.Black);
            scoreLabel.BackgroundImage = ResourceManager.GetVisualObject("Gem").PassiveImage;
            scoreLabel.BackgroundImageLayout = ImageLayout.Zoom;
            scoreLabel.TextAlign = ContentAlignment.BottomCenter;
            scoreLabel.Text = Game.CurrentLevel.GemCount.ToString();
            topPanel.Controls.Add(scoreLabel, 1, 0);

            InitializePurchasePanel();
            topPanel.Controls.Add(purchasePanel, 2, 0);

            var deleteHeroButton = FormUtils.GetButtonWithTextAndFontColor(
                "Удалить героя", Color.Teal, 13);
            deleteHeroButton.Margin = Padding.Empty;
            deleteHeroButton.Click += ActivateDeletion;
            topPanel.Controls.Add(deleteHeroButton, 3, 0);
                      
            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor(
                "В главное меню", Color.Blue, 13);
            exitToMenuButton.Margin = Padding.Empty;
            exitToMenuButton.Click += SwitchToMenu;
            topPanel.Controls.Add(exitToMenuButton, 4, 0);
            
            mainPanel.Controls.Add(topPanel, 0, 0);

            gamePanel = GetGamePanel();
            mainPanel.Controls.Add(gamePanel, 0, 1);           
            Controls.Add(mainPanel);
        }

        private void InitializePurchasePanel()
        {
            purchasePanel = FormUtils.GetTableLayoutPanel(1, 5);
            var i = 0;
            foreach (var hero in Game.CurrentLevel.AvailableHeroes)
            {
                var heroPurchase = FormUtils.GetButtonWithTextAndFontColor(
                    hero.Price.ToString(), Color.Black);
                heroPurchase.BackgroundImage = ResourceManager.GetVisualObject(hero.Type).PassiveImage;
                heroPurchase.BackgroundImageLayout = ImageLayout.Zoom;
                heroPurchase.TextAlign = ContentAlignment.BottomCenter;
                heroPurchase.Margin = Padding.Empty;
                heroPurchase.FlatStyle = FlatStyle.Flat;
                heroPurchase.FlatAppearance.BorderColor = Color.Blue;
                heroPurchase.FlatAppearance.BorderSize = 3;
                heroPurchase.Click += (sender, e) =>
                {
                    if (currentPurchaseButton != null)
                        currentPurchaseButton.FlatAppearance.BorderColor = Color.Blue;
                    currentPurchaseButton = heroPurchase;
                    currentPurchaseButton.FlatAppearance.BorderColor = Color.Green;
                    currentObjectToPurchase = hero;
                };
                purchasePanel.Controls.Add(heroPurchase, i++, 0);
            }
        }       
        
        private void ProcessMouseClick(object sender, MouseEventArgs e)
        {
            var locationInMap = CoordinatesUtils.GetLocationInMapByLocationInControl(new Vector(e.X, e.Y), cellSize);
            var gemToDelete = new List<Gem>();
            foreach (var gem in Game.CurrentLevel.Map.Gems)
            {                
                if ((gem.Position - locationInMap).Length < 0.9)
                {
                    Game.CurrentLevel.GemCount += 25;
                    gemToDelete.Add(gem);
                }
            }

            foreach (var gem in gemToDelete)
            {
                Game.CurrentLevel.Map.Delete(gem);
            }
            
            if (gemToDelete.Count > 0)
                return;
            
            if (isDeleteSelected)
            {
                foreach (var hero in Game.CurrentLevel.Map.GetHeroesFromLine((int)locationInMap.Y))
                {
                    if (Math.Abs(hero.Position.X - locationInMap.X) < double.Epsilon)
                        Game.CurrentLevel.Map.Delete(hero);
                }
                isDeleteSelected = false;
                return;
            }

            if (currentObjectToPurchase != null)
            {
                if (Game.CurrentLevel.GemCount >= currentObjectToPurchase.Price
                    && !Game.CurrentLevel.Map.Heroes
                        .Any(hero => (locationInMap - hero.Position).Length < 0.1))
                {
                    var ctor = currentObjectToPurchase.Type.GetConstructors()[0];
                    var heroToAdd = (IHero) ctor.Invoke(
                        new object[] {currentObjectToPurchase.Parameters, locationInMap});
                    Game.CurrentLevel.Map.Add(heroToAdd);
                    Game.CurrentLevel.GemCount -= currentObjectToPurchase.Price;
                }
            }
        }

        private void UpdateCellSize()
        {
            cellSize = new Size(field.Width / (Game.CurrentLevel.Map.Width - 2),
                field.Height / Game.CurrentLevel.Map.Height);
        }

        private void UpdateCellSize(object sender, EventArgs e) => UpdateCellSize();

        private TableLayoutPanel GetGamePanel()
        {
            gamePanel = FormUtils.GetTableLayoutPanel(1, 2);
            gamePanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
            gamePanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80F);

            var headquartersControl = new Label
            {
                BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\headquarters.jpg"),
                BackgroundImageLayout = ImageLayout.Stretch,
                Margin = Padding.Empty
            };
            FormUtils.SetAnchorForAllSides(headquartersControl);
            gamePanel.Controls.Add(headquartersControl, 0, 0);

            field = FormUtils.GetTransparentLabel();
            field.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\field.jpg");
            field.BackgroundImageLayout = ImageLayout.Stretch;
            field.Margin = Padding.Empty;
            UpdateCellSize();
            field.SizeChanged += UpdateCellSize;
            field.Paint += OnPaint;
            field.MouseClick += ProcessMouseClick;          
            
            gamePanel.Controls.Add(field, 1, 0);

            gameOverPanel = GetPanelEndGame("gameover.jpg", "Начать заново", Restart);
            levelWinPanel = GetPanelEndGame("winlevel.jpg", "Следующий уровень", ToNextLevel);
            gameWinPanel = GetPanelEndGame("wingame.jpg", "Начать заново", Restart);
            var gameWinLabel = FormUtils.GetLabelWithTextAndFontColor();
            gameWinLabel.BorderStyle = BorderStyle.None;
            gameWinPanel.Controls.Add(gameWinLabel, 1, 1);
            return gamePanel;
        }
        
        private TableLayoutPanel GetPanelEndGame(string imageName, string buttonText, EventHandler buttonHandler)
        {
            var resultPanel = FormUtils.GetTableLayoutPanel(5, 3);
            resultPanel.BackgroundImage = Image.FromFile(
                Environment.CurrentDirectory + $@"\Resources\{imageName}");
            resultPanel.BackgroundImageLayout = ImageLayout.Stretch;
            resultPanel.Margin = Padding.Empty;
            var button = FormUtils.GetButtonWithTextAndFontColor(
                buttonText, Color.Red, 20);
            button.Click += buttonHandler;
            resultPanel.Controls.Add(button, 1, 3);
            return resultPanel;
        }

        public void Restart(object sender, EventArgs e)
        {
            Game = gameFactory.Create(player);
            mainPanel.Controls.RemoveAt(1); 
            mainPanel.Controls.Add(gamePanel, 0, 1);
        }

        private void ToNextLevel(object sender, EventArgs e)
        {
            Game.ToNextLevel();
            mainPanel.Controls.RemoveAt(1); 
            mainPanel.Controls.Add(gamePanel, 0, 1);
            currentLevelLabel.Text = "Уровень " + (Game.CurrentLevelNumber + 1);
        }

        private void SwitchToMenu(object sender, EventArgs e)
        {
            Hide();
            MainMenuForm.Show(this);
        }

        private void ActivateDeletion(object sender, EventArgs e) => isDeleteSelected = true;

        private void OnTimer(object sender, EventArgs e)
        {
            Game.MakeGameIteration();
            field.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            scoreLabel.Text = Game.CurrentLevel.GemCount.ToString();
            DrawMap(sender, e);
        }
        
        private void OnFrameChanged(object sender, EventArgs e) => field.Invalidate();

        private void CheckPanelAlreadySet(TableLayoutPanel panel)
        {
            if (!mainPanel.Contains(panel))
            {
                mainPanel.Controls.Remove(gamePanel);
                mainPanel.Controls.Add(panel, 0, 1);
            }
        }

        private void DrawMap(object sender, PaintEventArgs e)
        {
            if (Game.IsWin)
            {
                CheckPanelAlreadySet(gameWinPanel);
                return;
            }
            
            if (Game.IsOver)
            {
                CheckPanelAlreadySet(gameOverPanel);
                return;
            }
            
            if (Game.CurrentLevel.IsWin)
            {
                ShopForm.UpdateCoinsLabel();
                CheckPanelAlreadySet(levelWinPanel);
                return;
            }
            
            foreach (var gameObject in Game.CurrentLevel.Map.GetGameObjects())
            {
                var visualObject = ResourceManager.GetVisualObject(gameObject.GetType());
                var currentAnimation = visualObject.PassiveImage;
                if (gameObject.State != State.Idle)
                {
                    currentAnimation = gameObject.State == State.Moves
                        ? visualObject.MoveImage
                        : visualObject.AttackImage;
                    if (!visualObject.Animations.ContainsKey(gameObject))
                    {
                        visualObject.Animations[gameObject] = new Animation(
                            false, currentAnimation);
                    }
                    AnimationUtils.AnimateImage(visualObject.Animations[gameObject], currentAnimation, OnFrameChanged);
                    ImageAnimator.UpdateFrames();               
                }
                var rectangleInMapControl = CoordinatesUtils.GetRectangleToPaintByLocationInMap(gameObject.Position, cellSize);
                if (gameObject is IStrike)
                {
                    rectangleInMapControl.Height /= 3;
                    rectangleInMapControl.Y += rectangleInMapControl.Height / 2;
                }
                e.Graphics.DrawImage(currentAnimation, rectangleInMapControl);
            }
        }
    }
}