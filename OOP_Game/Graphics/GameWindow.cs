using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using OOP_Game.GameLogic;
using OOP_Game.Units;
using Size = System.Drawing.Size;

namespace OOP_Game
{
    public partial class GameWindow : Form
    {
        private Game Game = GameFactory.GetStandardGame();
        private readonly Form mainMenu;
        private TableLayoutPanel fieldPanel;
        private Label currentLevelLabel;
        private Label scoreLabel;
        private TableLayoutPanel purchasePanel;
        private readonly Image gemImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\gem.jpg");
        private readonly ResourceManager resourceManager = new ResourceManager();
        
        public GameWindow()
        {
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint, true);
            UpdateStyles();
            Name = "GameForm";
            Text = "OOPGame";
            var timer = new Timer();
            timer.Interval = 40;
            timer.Tick += OnTimer;
            timer.Start();
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
            var i = 0;
            foreach (var hero in Game.CurrentLevel.availableHeroes)
            {

                // фейковый Железный Человек на панели
                var heroPurchase = FormUtils.GetButtonWithTextAndFontColor("50", Color.Black, 15);
                heroPurchase.BackgroundImage = resourceManager.VisualObjects[hero.Name].PassiveImage;
                heroPurchase.BackgroundImageLayout = ImageLayout.Zoom;
                heroPurchase.TextAlign = ContentAlignment.BottomCenter;
                heroPurchase.Margin = Padding.Empty;
                purchasePanel.Controls.Add(heroPurchase, i, 0);
                i++;
            }

            // добавить персонажей, доступных к покупке на панель
            
            
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
            var fakeLabel = FormUtils.GetTransparentLabel();
            fieldPanel.Controls.Add(fakeLabel, 0, 0);
            fieldPanel.Paint += OnPaint;
            
            // Именно внутри fieldControl и будут происходить основные события игры           
            
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

        private void OnTimer(object sender, EventArgs e)
        {
            Game.MakeGameIteration();
            fieldPanel.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
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


        private void DrawMap(object sender, PaintEventArgs e)
        {
            if(Game.GameIsOver)
               return; 
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
