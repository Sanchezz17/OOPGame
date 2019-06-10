using System.Windows.Forms;
using Ninject;
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
            var container = CreateContainer();
            Application.Run(container.Get<GameForm>());
        }

        public static StandardKernel CreateContainer()
        {
            var container = new StandardKernel();
            var player = new Player();
            container.Bind<Player>().ToConstant(player).InSingletonScope();
            container.Bind<GameForm>().ToSelf().InSingletonScope();
            container.Bind<ResourceManager>().ToSelf().InSingletonScope();
            container.Bind<Game>().ToConstant(GameFactory.GetStandardGame(player)).InSingletonScope();
            return container;
        }
    }
}