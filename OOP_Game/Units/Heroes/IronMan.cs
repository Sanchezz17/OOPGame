using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Heroes
{
    public class IronMan : IHero
    {
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get;  set; }
        public bool IsDead { get; private set; }
        public int RechargeTimeInTicks { get; set; }

        public IronMan(int health, Vector position)
        {
            Health = health;
            Position = position;
            State = State.Idle;
            RechargeTimeInTicks = 15;
        }
        
        public void Trigger(IStrike strike)
        {
            Health -= strike.ToDamage();
            if (Health <= 0)
                IsDead = true;
        }

        public IStrike Attack()
        {
            return new Shot(1, Position + Direction.Right.ToVector() / 2,
                Infrastructure.Direction.Right);
        }
    }
}