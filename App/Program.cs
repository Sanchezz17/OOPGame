using System.Windows.Forms;
using App.Graphics;
using Domain.GameLogic;

namespace App
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow(GameFactory.GetStandardGame(), new ResourceManager()));
        }
    }
}