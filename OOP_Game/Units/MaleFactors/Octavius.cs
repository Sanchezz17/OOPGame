using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units
{
    public class Octavius : IMalefactor
    {
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        public Direction Direction { get; }
        
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;

        public Octavius(int health, Vector position)
        {
            Health = health;
            Position = position;
            IsDead = false;
            State = State.Moves;
            rechargeTimeInTicks = 15;
            baseRechargeTimeInTicks = 15;
        }
        
        public void Move()
        {
            Position += Direction.ToVector() * 0.025;
        }

        public IStrike Attack()
        {
            return new IronManAttack(3, Position, Direction);
        }

        public bool IsAttackAvailable()
        {
            if (rechargeTimeInTicks == 0)
            {
                rechargeTimeInTicks = baseRechargeTimeInTicks;
                return true;
            }
            rechargeTimeInTicks--;
            return false;
        }

        public void Trigger(IStrike strike)
        {
            var parametres = new UnitParameters(Health, Position, State, IsDead, baseRechargeTimeInTicks);
            Health -= strike.ToDamage(parametres);
            if (Health <= 0)
                IsDead = true;
        }
    }
}