using System;
using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public class GemFactory : IGemManufacturer
    {
        private TickСontroller tickСontroller;
        private Random random;
        
        public int Health { get; }
        public Vector Position { get; }
        public State State { get; set; }
        public bool IsDead { get; }
        public GemFactory()
        {
            tickСontroller = new TickСontroller(150);
            random = new Random();
        }

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem() => new Gem(new Vector(random.NextDouble() * 9, 0), 0.0125);
    }
}