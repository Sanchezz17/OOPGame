using System.Windows.Forms;
using Ninject;
using App.Graphics;
using Domain.GameLogic;
using OOP_Game.GameLogic;

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
            container.Bind<Player>().ToSelf().InSingletonScope();
            container.Bind<GameForm>().ToSelf().InSingletonScope();
            container.Bind<ResourceManager>().ToSelf().InSingletonScope();
            container.Bind<IGameFactory>().To<FactoryViaApi>();
            return container;
        }
    }
}