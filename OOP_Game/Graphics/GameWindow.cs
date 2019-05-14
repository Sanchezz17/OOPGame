using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using OOP_Game.GameLogic;
using OOP_Game.Infrastructure;
using OOP_Game.Units;
using Size = System.Drawing.Size;

namespace OOP_Game
{
    public partial class GameWindow : Form
    {
        public Game Game { get; set; }
        private readonly Form mainMenu;
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel gamePanel;
        private TableLayoutPanel fieldPanel;
        private TableLayoutPanel gameOverPanel;
        private Label currentLevelLabel;
        private Label scoreLabel;
        private TableLayoutPanel purchasePanel;
        private ResourceManager resourceManager = new ResourceManager();
        private PurchaseObject currentObjectToPurchase = null;
        
        public GameWindow()
        {
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint, true);
            UpdateStyles();
            Name = "GameForm";
            Text = "OOPGame";
            Game = GameFactory.GetStandardGame();
            var timer = new Timer();
            timer.Interval = 40;
            timer.Tick += OnTimer;
            timer.Start();
            Size = new Size(950, 700);
            mainMenu = new MainMenu(this);
            Shown += SwitchToMenu;
            InitializeGameWindow();
        }

        private void InitializeGameWindow()
        {
            mainPanel = FormUtils.InitializeTableLayoutPanel(2, 1);
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
            scoreLabel.BackgroundImage = resourceManager.VisualObjects["Gem"].PassiveImage;
            scoreLabel.BackgroundImageLayout = ImageLayout.Zoom;
            scoreLabel.TextAlign = ContentAlignment.BottomCenter;
            scoreLabel.Text = Game.CurrentLevel.GemCount.ToString();
            topPanel.Controls.Add(scoreLabel, 1, 0);


            InitializePurchasePanel();
            topPanel.Controls.Add(purchasePanel, 2, 0);
            
            var exitToMenuButton = FormUtils.GetButtonWithTextAndFontColor("В главное меню", Color.Blue, 15);
            exitToMenuButton.Margin = Padding.Empty;
            exitToMenuButton.Click += SwitchToMenu;
            topPanel.Controls.Add(exitToMenuButton, 3, 0);
            
            mainPanel.Controls.Add(topPanel, 0, 0);

            var gamePanel = GetGamePanel();
            mainPanel.Controls.Add(gamePanel, 0, 1);           
            Controls.Add(mainPanel);
        }

        private void InitializePurchasePanel()
        {
            purchasePanel = FormUtils.InitializeTableLayoutPanel(1, 5);
            var i = 0;
            foreach (var hero in Game.CurrentLevel.AvailableHeroes)
            {
                var heroPurchase = FormUtils.GetButtonWithTextAndFontColor(
                    hero.Price.ToString(), Color.Black, 15);
                heroPurchase.BackgroundImage = resourceManager.VisualObjects[hero.Type.Name].PassiveImage;
                heroPurchase.BackgroundImageLayout = ImageLayout.Zoom;
                heroPurchase.TextAlign = ContentAlignment.BottomCenter;
                heroPurchase.Margin = Padding.Empty;
                heroPurchase.Click += (sender, e) => currentObjectToPurchase = hero;
                purchasePanel.Controls.Add(heroPurchase, i++, 0);
            }
        }       
        
        private void ProcessMouseClick(object sender, MouseEventArgs e)
        {
            var coordinatesInMap = CoordinatesInLayoutToMap(new Vector(e.X, e.Y));
            if (currentObjectToPurchase != null 
                && Game.CurrentLevel.GemCount >= currentObjectToPurchase.Price
                && !Game.CurrentLevel.Map.ForEachHeroes().Any(hero => (coordinatesInMap - hero.Position).Length < 0.1))
            {
                
                var ctor = currentObjectToPurchase.Type.GetConstructors()[0];
                var heroToAdd = (IHero)ctor.Invoke(
                    new object[] {currentObjectToPurchase.Health, coordinatesInMap});
                Game.CurrentLevel.Map.Add(heroToAdd);
                Game.CurrentLevel.GemCount -= currentObjectToPurchase.Price;
                currentObjectToPurchase = null;
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
            gamePanel = FormUtils.InitializeTableLayoutPanel(1, 2);
            gamePanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 20F);
            gamePanel.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 80F);

            var headquartersControl = new Label();
            headquartersControl.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\headquarters.jpg");
            headquartersControl.BackgroundImageLayout = ImageLayout.Stretch;
            FormUtils.SetAnchorForAllSides(headquartersControl);
            headquartersControl.Margin = Padding.Empty;
            gamePanel.Controls.Add(headquartersControl, 0, 0);

            fieldPanel = FormUtils.InitializeTableLayoutPanel(5, 9);
            fieldPanel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\field.jpg");
            fieldPanel.BackgroundImageLayout = ImageLayout.Stretch;
            fieldPanel.Margin = Padding.Empty;
            // фейковый label для размеров клетки
            var fakeLabel = FormUtils.GetTransparentLabel();
            fakeLabel.MouseClick += ProcessMouseClick;
            fieldPanel.Controls.Add(fakeLabel, 0, 0);
            fieldPanel.Paint += OnPaint;
            fieldPanel.MouseClick += ProcessMouseClick;
            
            // Именно внутри fieldControl и будут происходить основные события игры           
            
            gamePanel.Controls.Add(fieldPanel, 1, 0);
            
            InitializeGameOverPanel();
            return gamePanel;
        }

        private void InitializeGameOverPanel()
        {
            gameOverPanel = FormUtils.InitializeTableLayoutPanel(5, 3);
            gameOverPanel.BackgroundImage = Image.FromFile(
                Environment.CurrentDirectory + @"\Resources\gameover.jpg");
            gameOverPanel.BackgroundImageLayout = ImageLayout.Stretch;
            gameOverPanel.Margin = Padding.Empty;
            var restartGameButton = FormUtils.GetButtonWithTextAndFontColor(
                "Начать заново", Color.Red, 20);
            restartGameButton.Click += Restart;
            gameOverPanel.Controls.Add(restartGameButton, 1, 3);
        }

        private void Restart(object sender, EventArgs e)
        {
            resourceManager = new ResourceManager();
            Game = GameFactory.GetStandardGame();
            Game.Start();
            mainPanel.Controls.Remove(gameOverPanel);  
            mainPanel.Controls.Add(gamePanel, 0, 1);
        }

        private void SwitchToMenu(object sender, EventArgs e)
        {
            Game.Pause();
            Hide();
            mainMenu.Location = Location;
            mainMenu.Size = Size;
            mainMenu.Show();
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
        
        private void OnFrameChanged(object o, EventArgs e) => fieldPanel.Invalidate();
        
        private void AnimateImage(Animation animation, Bitmap currentAnimation)
        {
            if (animation.CurrentAnimation != currentAnimation)
                animation.CurrentlyAnimating = false;
            if (!animation.CurrentlyAnimating)
            {
                ImageAnimator.Animate(currentAnimation, new EventHandler(OnFrameChanged));
                animation.CurrentlyAnimating = true;
                animation.CurrentAnimation = currentAnimation;
            }
        }
        
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

        private void DrawMap(object sender, PaintEventArgs e)
        {
            if (Game.GameIsOver)
            {
                if (!mainPanel.Contains(gameOverPanel))
                {
                    mainPanel.Controls.Remove(gamePanel);
                    mainPanel.Controls.Add(gameOverPanel, 0, 1);
                }
                return;
            }
            foreach (var gameObject in Game.CurrentLevel.Map.ForEachGameObject())
            {
                var visualObject = resourceManager.VisualObjects[gameObject.GetType().Name];
                var currentAnimation = visualObject.PassiveImage;
                if (gameObject.State != State.Idle)
                {
                    currentAnimation = gameObject.State == State.Attacks
                        ? visualObject.AttackImage
                        : visualObject.MoveImage;
                    if (!visualObject.Animations.ContainsKey(gameObject))
                    {
                        visualObject.Animations[gameObject] = new Animation(
                            false, currentAnimation);
                    }
                    AnimateImage(visualObject.Animations[gameObject], currentAnimation);
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
