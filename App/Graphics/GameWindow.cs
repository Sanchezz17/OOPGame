using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Domain.GameLogic;
using Domain.Infrastructure;
using Domain.Units;
using WMPLib;
using Size = System.Drawing.Size;

namespace App
{
    public partial class GameWindow : Form
    {
        public Game Game { get; set; }
        public ResourceManager ResourceManager { get; set; }
        public readonly Form MainMenu;
        public readonly Form ShopForm;
        
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel gamePanel;
        private TableLayoutPanel fieldPanel;
        private TableLayoutPanel gameOverPanel;
        private TableLayoutPanel levelWinPanel;
        private Label currentLevelLabel;
        private Label scoreLabel;
        private TableLayoutPanel purchasePanel;
        private DescribeObject currentObjectToPurchase;
        private Button currentPurchaseButton;
        private bool IsDelete;
        private WindowsMediaPlayer audioPlayer;
        
        public GameWindow(Game game, ResourceManager resourceManager)
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                      | ControlStyles.UserPaint, true);
            UpdateStyles();
            Name = "GameForm";
            Text = "OOPGame";
            Game = game;
            ResourceManager = resourceManager;
            var timer = new Timer {Interval = 40};
            timer.Tick += OnTimer;
            timer.Start();
            Size = new Size(950, 700);
            MainMenu = new MainMenu(this);
            ShopForm = new ShopForm(this);
            Shown += SwitchToMenu;
            InitializeGameWindow();
            audioPlayer = new WindowsMediaPlayer();
            audioPlayer.URL = Environment.CurrentDirectory + @"\Resources\Music\soundtrack.mp3";
        }

        public void PlaySoundrack()
        {
            audioPlayer.settings.volume = 100; // меняя значение можно регулировать громкость
            audioPlayer.controls.play(); // Старт
            //audioPlayer.controls.stop(); // Стоп
            //audioPlayer.close();
        }

        private void InitializeGameWindow()
        {
            mainPanel = FormUtils.GetTableLayoutPanel(2, 1);
            mainPanel.Size = new Size(Size.Width - 15, Size.Height - 40);
            mainPanel.Location = Location;
            FormUtils.SplitRowsByPercentages(mainPanel.RowStyles,
                new float[] {25F, 75F});

            var topPanel = FormUtils.GetTableLayoutPanel(1, 5);
            FormUtils.SplitColumnsByPercentages(
                topPanel.ColumnStyles, new float[]{10F, 10F, 60F, 10F, 10F});

            currentLevelLabel = FormUtils.GetLabelWithTextAndFontColor(
                "Уровень " + (Game.CurrentLevelNumber + 1).ToString(),
                Color.Black, 13);
            topPanel.Controls.Add(currentLevelLabel, 0, 0);

            scoreLabel = FormUtils.GetLabelWithTextAndFontColor("", Color.Black);
            scoreLabel.BackgroundImage = ResourceManager.VisualObjects["Gem"].PassiveImage;
            scoreLabel.BackgroundImageLayout = ImageLayout.Zoom;
            scoreLabel.TextAlign = ContentAlignment.BottomCenter;
            scoreLabel.Text = Game.CurrentLevel.GemCount.ToString();
            topPanel.Controls.Add(scoreLabel, 1, 0);

            InitializePurchasePanel();
            topPanel.Controls.Add(purchasePanel, 2, 0);

            var deleteHeroButton = FormUtils.GetButtonWithTextAndFontColor(
                "Удалить героя", Color.Teal, 13);
            deleteHeroButton.Margin = Padding.Empty;
            deleteHeroButton.Click += DeleteHero;
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
                    hero.Price.ToString(), Color.Black, 15);
                heroPurchase.BackgroundImage = ResourceManager.VisualObjects[hero.Type.Name].PassiveImage;
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
            var coordinatesInMap = CoordinatesInLayoutToMap(new Vector(e.X, e.Y));
            if (IsDelete)
            {
                foreach (var hero in Game.CurrentLevel.Map.GetHeroesFromLine((int)coordinatesInMap.Y))
                {
                    if (Math.Abs(hero.Position.X - coordinatesInMap.X) < double.Epsilon)
                        Game.CurrentLevel.Map.Delete(hero);
                }
                IsDelete = false;
                return;
            }

            if (currentObjectToPurchase != null)
            {
                if (Game.CurrentLevel.GemCount >= currentObjectToPurchase.Price
                    && !Game.CurrentLevel.Map.Heroes
                        .Any(hero => (coordinatesInMap - hero.Position).Length < 0.1))
                {
                    var ctor = currentObjectToPurchase.Type.GetConstructors()[0];
                    var heroToAdd = (IHero) ctor.Invoke(
                        new object[] {currentObjectToPurchase.Parameters, coordinatesInMap});
                    Game.CurrentLevel.Map.Add(heroToAdd);
                    Game.CurrentLevel.GemCount -= currentObjectToPurchase.Price;
                }
            }

            var gemToDelete = new List<Gem>();
            foreach (var gem in Game.CurrentLevel.Map.Gems)
            {                
                if ((gem.Position - coordinatesInMap).Length < 0.9)
                {
                    Game.CurrentLevel.GemCount += 25;
                    gemToDelete.Add(gem);
                }
            }

            foreach (var gem in gemToDelete)
            {
                Game.CurrentLevel.Map.Delete(gem);
            }
        }      

        private TableLayoutPanel GetGamePanel()
        {
            gamePanel = FormUtils.GetTableLayoutPanel(1, 2);
            gamePanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
            gamePanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80F);

            var headquartersControl = new Label();
            headquartersControl.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\headquarters.jpg");
            headquartersControl.BackgroundImageLayout = ImageLayout.Stretch;
            FormUtils.SetAnchorForAllSides(headquartersControl);
            headquartersControl.Margin = Padding.Empty;
            gamePanel.Controls.Add(headquartersControl, 0, 0);

            fieldPanel = FormUtils.GetTableLayoutPanel(5, 9);
            fieldPanel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\field.jpg");
            fieldPanel.BackgroundImageLayout = ImageLayout.Stretch;
            fieldPanel.Margin = Padding.Empty;
            // фейковый label для размеров клетки
            var fakeLabel = FormUtils.GetTransparentLabel();
            fakeLabel.MouseClick += ProcessMouseClick;
            fieldPanel.Controls.Add(fakeLabel, 0, 0);
            fieldPanel.Paint += OnPaint;
            fieldPanel.MouseClick += ProcessMouseClick;          
            
            gamePanel.Controls.Add(fieldPanel, 1, 0);

            gameOverPanel = GetPanelEndGame("gameover", "Начать заново", Restart);
            levelWinPanel = GetPanelEndGame("winlevel", "Следующий уровень", ToNextLevel);
            return gamePanel;
        }
        
        private TableLayoutPanel GetPanelEndGame(string imageName, string buttonText, EventHandler buttonHandler)
        {
            var resultPanel = FormUtils.GetTableLayoutPanel(5, 3);
            resultPanel.BackgroundImage = Image.FromFile(
                Environment.CurrentDirectory + $@"\Resources\{imageName}.jpg");
            resultPanel.BackgroundImageLayout = ImageLayout.Stretch;
            resultPanel.Margin = Padding.Empty;
            var button = FormUtils.GetButtonWithTextAndFontColor(
                buttonText, Color.Red, 20);
            button.Click += buttonHandler;
            resultPanel.Controls.Add(button, 1, 3);
            return resultPanel;
        }

        private void Restart(object sender, EventArgs e)
        {
            ResourceManager = new ResourceManager();
            Game = GameFactory.GetStandardGame();
            Game.Start();
            mainPanel.Controls.Remove(gameOverPanel);  
            mainPanel.Controls.Add(gamePanel, 0, 1);
        }

        private void ToNextLevel(object sender, EventArgs e)
        {
            //переход на следующий уровень
        }

        private void SwitchToMenu(object sender, EventArgs e)
        {
            audioPlayer.controls.stop();
            Game.Pause();
            Hide();
            MainMenu.Location = Location;
            MainMenu.Size = Size;
            MainMenu.Show();
        }

        private void DeleteHero(object sender, EventArgs e)
        {
            IsDelete = true;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            if (Game.Started)
            {
                Game.MakeGameIteration();
                fieldPanel.Invalidate();
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            scoreLabel.Text = Game.CurrentLevel.GemCount.ToString();
            DrawMap(sender, e);
        }
        
        private void OnFrameChanged(object sender, EventArgs e) => fieldPanel.Invalidate();
        
        private RectangleF GetCoordinatesInMapLayout(Vector location)
        {
            var cell = fieldPanel.GetControlFromPosition(0, 0);
            var x = (float)(location.X * cell.Size.Width);
            var y = (float)(location.Y * cell.Size.Height);
            return new RectangleF(x, y, cell.Width, cell.Height);
        }

        private Vector CoordinatesInLayoutToMap(Vector coordinatesInLayout)
        {
            var cell = fieldPanel.GetControlFromPosition(0, 0);
            var x = Math.Truncate(coordinatesInLayout.X / cell.Size.Width);
            var y = Math.Truncate(coordinatesInLayout.Y / cell.Size.Height);
            return new Vector(x, y);
        }

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
            if (Game.GameIsOver)
            {
                CheckPanelAlreadySet(gameOverPanel);
                return;
            }
            
            if (Game.CurrentLevel.IsWin)
            {
                CheckPanelAlreadySet(levelWinPanel);
                return;
            }
            
            foreach (var gameObject in Game.CurrentLevel.Map.GetGameObjects())
            {
                var visualObject = ResourceManager.VisualObjects[gameObject.GetType().Name];
                var currentAnimation = visualObject.PassiveImage;
                if (gameObject.State != State.Idle)
                {
                    currentAnimation = gameObject.State == State.Attacks || gameObject.State == State.Produce
                        ? visualObject.AttackImage
                        : visualObject.MoveImage;
                    if (!visualObject.Animations.ContainsKey(gameObject))
                    {
                        visualObject.Animations[gameObject] = new Animation(
                            false, currentAnimation);
                    }
                    AnimationUtils.AnimateImage(visualObject.Animations[gameObject], currentAnimation, OnFrameChanged);
                    ImageAnimator.UpdateFrames();               
                }
                var rectangleInMapLayout = GetCoordinatesInMapLayout(gameObject.Position);
                if (gameObject is IStrike)
                {
                    rectangleInMapLayout.Height /= 3;
                    rectangleInMapLayout.Y += rectangleInMapLayout.Height / 2;
                }
                e.Graphics.DrawImage(currentAnimation, rectangleInMapLayout);
            }
        }
    }
}