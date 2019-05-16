using System.Windows;
using Domain.Infrastructure;

namespace Domain.Units
{
    public abstract class BaseMalefactor : IMalefactor
    {
        public int Health { get; private set; }
        public Vector Position { get; protected set; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        public Direction Direction { get; }
        public double Speed { get; }
        private TickСontroller tickСontroller;

        protected BaseMalefactor(int health, Vector position, State state, double speed, int countTick)
        {
            Health = health;
            Position = position;
            State = state;
            IsDead = false;
            Direction = Direction.Left;
            Speed = speed;
            tickСontroller = new TickСontroller(countTick);
        }
        
        public abstract IStrike Attack();
        
        public void Move() => Position += Direction.ToVector() * Speed;

        public bool IsAttackAvailable() => tickСontroller.Check();

        public void Trigger(IStrike strike)
        {
            var parameters = new UnitParameters().SetHealth(Health);
            Health -= strike.ToDamage(parameters);
            if (Health <= 0)
                IsDead = true;
        }
    }
}