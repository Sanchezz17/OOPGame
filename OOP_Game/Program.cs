using System;
using System.Windows.Forms;
using OOP_Game.GameLogic;

namespace OOP_Game
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow(GameFactory.GetStandardGame(), new ResourceManager()));
        }
        
        
    }
}
