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
            InitializeMainMenu();
            Show();
        }       
        
        private void InitializeMainMenu()
        {
            var playButton = FormUtils.GetButtonWithTextAndFontColor("Новая игра", Color.Red, 20);
            playButton.Click += NewPlayClick;
            var loadButton = FormUtils.GetButtonWithTextAndFontColor("Загрузить", Color.Blue, 20);
            loadButton.Click += LoadClick;
            var exitButton = FormUtils.GetButtonWithTextAndFontColor("Выход", Color.Black, 20);
            exitButton.Click += ExitClick;

            var panel = FormUtils.InitializeTableLayoutPanel(5, 3);
            panel.Controls.Add(playButton, 1, 1);
            panel.Controls.Add(loadButton, 1, 2);
            panel.Controls.Add(exitButton, 1, 3);
            panel.Size = Size;
            
            var url = "https://up.enterdesk.com/edpic_360_360/db/7f/bf/db7fbf0a45eccdf1b03aeb493bc9d2ec.jpg";
            using (WebClient webClient = new WebClient())
            {
                panel.BackgroundImage = Image.FromStream(webClient.OpenRead(url));
            }
            panel.BackgroundImageLayout = ImageLayout.Zoom;
            
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
        
        private void ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}