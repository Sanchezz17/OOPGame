using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Heroes
{
    public class Vision : IHero, IGemManufacturer
    {
        public int Health { get; private set; }
        public Vector Position { get; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        private TickСontroller tickСontroller;

        public Vision(int health, Vector position)
        {
            Health = health;
            Position = position;
            State = State.Produce;
            IsDead = false;
            tickСontroller = new TickСontroller(150);
        }
        public void Trigger(IStrike strike)
        {
            var parametres = new UnitParameters(Health, Position, State, IsDead);
            Health -= strike.ToDamage(parametres);
            if (Health <= 0)
                IsDead = true;
        }

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem()
        {
            return new Gem(Position, 0);
        }
    }
}