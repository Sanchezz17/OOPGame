using System.Linq;
using System.Windows;
using Domain.Infrastructure;
using Domain.Units;

namespace Domain.Units.Heroes
{
    public abstract class BaseHero : IHero
    {
        public int Health { get; private set; }
        public int Reload { get; private set; }
        public Vector Position { get; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        protected readonly TickСontroller tickСontroller;

        public BaseHero(UnitParameters parametres, Vector position, State state, int countTick)
        {
            Health = parametres.Health;
            if (parametres.characteristics.Where(c => c.Name == "Reload").Any())
                Reload = parametres.Reload;
            Position = position;
            State = state;
            IsDead = false;
            tickСontroller = new TickСontroller(Reload);
        }

        public void Trigger(IStrike strike)
        {
            var parameters = new UnitParameters().SetHealth(Health);
            Health -= strike.ToDamage(parameters);
            if (Health <= 0)
                IsDead = true;
        }
    }
}