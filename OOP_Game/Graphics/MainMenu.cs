using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Net.Mime;
using OOP_Game.GameLogic;

namespace OOP_Game
{
    public class MainMenu : Form
    {
        private GameWindow gameForm;
        private Button continueButton;
        
        public MainMenu(GameWindow gameForm)
        {
            this.gameForm = gameForm;
            Name = "MainMenu";
            Text = "Главное меню";
            Closed += OnExit;
            InitializeMainMenu();
        }       
        
        private void InitializeMainMenu()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            continueButton = FormUtils.GetButtonWithTextAndFontColor("Продолжить", Color.Red, 20);
            var newGameButton = FormUtils.GetButtonWithTextAndFontColor("Новая игра", Color.Blue, 20);
            newGameButton.Click += NewGameClick;
            var exitButton = FormUtils.GetButtonWithTextAndFontColor("Выход", Color.Black, 20);
            exitButton.Click += OnExit;

            var panel = FormUtils.InitializeTableLayoutPanel(5, 3);
            panel.Controls.Add(continueButton, 1, 1);
            panel.Controls.Add(newGameButton, 1, 2);
            panel.Controls.Add(exitButton, 1, 3);
            panel.Size = Size;

            panel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\main.jpg");
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            
            Controls.Add(panel);
        }
        
        private void PlayClick(object sender, EventArgs e)
        {
            gameForm.Location = Location;
            gameForm.Show();
            gameForm.Game.Start();
            Hide();
        }

        private void NewGameClick(object sender, EventArgs e)
        {
            gameForm.Game = GameFactory.GetStandardGame();
            PlayClick(sender,  e);
            continueButton.Click += PlayClick;
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}