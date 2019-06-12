using System;
using System.Drawing;
using System.Windows.Forms;

namespace App.Visualization
{
    public class MainMenuForm : DBForm
    {
        private readonly GameForm gameForm;
        private Button continueButton;
        
        public MainMenuForm(GameForm gameForm)
        {
            this.gameForm = gameForm;
            Text = "Главное меню";
            Closed += OnExit;
            InitializeMainMenu();
        }       
        
        private void InitializeMainMenu()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Location = gameForm.Location;
            Size = gameForm.Size;            
            
            continueButton = FormUtils.GetButtonWithTextAndFontColor("Продолжить", Color.Red, 20);
            var newGameButton = FormUtils.GetButtonWithTextAndFontColor("Новая игра", Color.Blue, 20);
            newGameButton.Click += NewGameClick;
            var exitButton = FormUtils.GetButtonWithTextAndFontColor("Выход", Color.Black, 20);
            exitButton.Click += OnExit;
            var shopButton = FormUtils.GetButtonWithTextAndFontColor("Магазин", Color.Teal, 20);
            shopButton.Click += ShopClick;
            
            var panel = FormUtils.GetTableLayoutPanel(6, 3);
            panel.Controls.Add(continueButton, 1, 1);
            panel.Controls.Add(newGameButton, 1, 2);
            panel.Controls.Add(shopButton, 1, 3);
            panel.Controls.Add(exitButton, 1, 4);
            panel.Size = Size;

            panel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\main.jpg");
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            
            Controls.Add(panel);
        }
        
        private void PlayClick(object sender, EventArgs e)
        {
            Hide();
            gameForm.Show(this);
        }

        private void ShopClick(object sender, EventArgs e)
        {
            Hide();
            gameForm.ShopForm.Show(this);
        }

        private void NewGameClick(object sender, EventArgs e)
        {
            gameForm.Restart(sender, e);
            PlayClick(sender,  e);
            continueButton.Click += PlayClick;
        }

        private void OnExit(object sender, EventArgs e) => Application.Exit();
    }
}