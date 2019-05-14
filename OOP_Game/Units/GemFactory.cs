using System;
using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public class GemFactory : IGemManufacturer
    {
        private TickСontroller tickСontroller;
        private Random random;
        public GemFactory()
        {
            tickСontroller = new TickСontroller(150);
            random = new Random();
        }

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem()
        {
            return new Gem(new Vector(random.NextDouble() * 9, 0), 0.0125);
        }
    }
}