using System;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Net.Mime;

namespace OOP_Game
{
    public class MainMenu : Form
    {
        private Form parent;
        
        public MainMenu(Form parent)
        {
            this.parent = parent;
            Name = "MainMenu";
            Text = "Главное меню";
            Closed += OnExit;
            InitializeMainMenu();
            Show();
        }       
        
        private void InitializeMainMenu()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            var playButton = FormUtils.GetButtonWithTextAndFontColor("Новая игра", Color.Red, 20);
            playButton.Click += NewPlayClick;
            var loadButton = FormUtils.GetButtonWithTextAndFontColor("Загрузить", Color.Blue, 20);
            loadButton.Click += LoadClick;
            var exitButton = FormUtils.GetButtonWithTextAndFontColor("Выход", Color.Black, 20);
            exitButton.Click += OnExit;

            var panel = FormUtils.InitializeTableLayoutPanel(5, 3);
            panel.Controls.Add(playButton, 1, 1);
            panel.Controls.Add(loadButton, 1, 2);
            panel.Controls.Add(exitButton, 1, 3);
            panel.Size = Size;

            panel.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\Resources\main.jpg");
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            
            Controls.Add(panel);
        }
        
        private void NewPlayClick(object sender, EventArgs e)
        {
            parent.Location = Location;
            parent.Show();
            Hide();
        }
        
        private void LoadClick(object sender, EventArgs e)
        {
            // загрузка сохранения
        }
        
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}