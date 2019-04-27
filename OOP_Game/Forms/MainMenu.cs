using System;
using System.Windows.Forms;
using System.Drawing;

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
            var playButton = GetButtonWithTextAndFontColor("Новая игра", Color.Red);
            playButton.Click += NewPlayClick;
            var loadButton = GetButtonWithTextAndFontColor("Загрузить", Color.Blue);
            loadButton.Click += LoadClick;
            var exitButton = GetButtonWithTextAndFontColor("Выход", Color.Black);
            exitButton.Click += ExitClick;

            var panel = InitializeTableLayoutPanel(5, 3);
            panel.Controls.Add(playButton, 1, 1);
            panel.Controls.Add(loadButton, 1, 2);
            panel.Controls.Add(exitButton, 1, 3);
            panel.Size = Size;
            Controls.Add(panel);
        }

        public static TableLayoutPanel InitializeTableLayoutPanel(int rowsCount, int columnsCount)
        {
            var panel = new TableLayoutPanel();
            
            panel.ColumnCount = columnsCount;
            var columnSizeInPercent = 100 / panel.ColumnCount;
            for (var i = 0; i < panel.ColumnCount; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnSizeInPercent));           
            
            panel.RowCount = rowsCount;
            var rowSizeInPercent = 100 / panel.RowCount;
            for (var i = 0; i < panel.RowCount; i++)
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, rowSizeInPercent));

            panel.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                            AnchorStyles.Top | AnchorStyles.Bottom);
            return panel;
        }

        public static Button GetButtonWithTextAndFontColor(string text, Color fontColor)
        {
            var button = new Button
            {
                Anchor = (AnchorStyles.Left | AnchorStyles.Right | 
                          AnchorStyles.Top | AnchorStyles.Bottom),
                ForeColor = fontColor,
                Text = text,
                Font = new Font("Arial", 20),
                UseVisualStyleBackColor = true
            };
            return button;
        }
        
        private void NewPlayClick(object sender, EventArgs e)
        {
            parent.Show();
            Close();
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