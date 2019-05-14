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
        
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;

        public Vision(int health, Vector position)
        {
            Health = health;
            Position = position;
            State = State.Produce;
            IsDead = false;
            baseRechargeTimeInTicks = 150;
            rechargeTimeInTicks = baseRechargeTimeInTicks;
        }
        public void Trigger(IStrike strike)
        {
            var parametres = new UnitParameters(Health, Position, State, IsDead, baseRechargeTimeInTicks);
            Health -= strike.ToDamage(parametres);
            if (Health <= 0)
                IsDead = true;
        }

        public bool IsAvailable()
        {
            if (rechargeTimeInTicks == 0)
            {
                rechargeTimeInTicks = baseRechargeTimeInTicks;
                return true;
            }
            rechargeTimeInTicks--;
            return false;
        }

        public Gem GetGem()
        {
            return new Gem(Position, 0);
        }
    }
}