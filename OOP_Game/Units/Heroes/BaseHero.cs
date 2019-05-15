using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Heroes
{
    public abstract class BaseHero :IHero
    {
        public int Health { get; private set; }
        public Vector Position { get; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        protected readonly TickСontroller tickСontroller;

        public BaseHero(int health, Vector position, State state, int countTick)
        {
            Health = health;
            Position = position;
            State = state;
            IsDead = false;
            tickСontroller = new TickСontroller(countTick);
        }

        public void Trigger(IStrike strike)
        {
            var parameters = new UnitParameters(Health, Position, State, IsDead);
            Health -= strike.ToDamage(parameters);
            if (Health <= 0)
                IsDead = true;
        }
    }
}