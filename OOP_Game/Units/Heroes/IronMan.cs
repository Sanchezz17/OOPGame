using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units.Heroes
{
    public class IronMan : IHero, IAttacking
    {
        public UnitParameters parametres { get; private set; }
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get;  set; }
        public bool IsDead { get; private set; }
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;

        public IronMan(int health, Vector position)
        {
            Health = health;
            Position = position;
            State = State.Idle;
            rechargeTimeInTicks = 15;
            baseRechargeTimeInTicks = 15;
        }
        
        public void Trigger(IStrike strike)
        {
            var parametres = new UnitParameters(Health, Position, State, IsDead, baseRechargeTimeInTicks);
            Health -= strike.ToDamage(parametres);
            if (Health <= 0)
                IsDead = true;
        }

        public IStrike Attack()
        {
            return new IronManAttack(10, Position + Direction.Right.ToVector() / 2,
                Direction.Right);
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
    }
}