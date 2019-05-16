using System.Windows.Forms;
using Domain.GameLogic;

namespace App
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow(GameFactory.GetStandardGame(), new ResourceManager()));
        }
    }
}