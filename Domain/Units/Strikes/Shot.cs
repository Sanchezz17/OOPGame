using System.Windows;
using Domain.Infrastructure;

namespace Domain.Units
{
    public abstract class Shot : IStrike, IMovable
    {
        public Direction Direction { get; }
        public int Health{ get; set; }
        public Vector Position { get; set; }
        public bool IsDead { get; set; }
        public int Damage { get; }
        public State State { get; set; }
        public double Speed => 0.5;

        protected Shot(Direction direction, int health, Vector position, int damage = 1)
        {
            Direction = direction;
            Health = health;
            Position = position;
            IsDead = false;
            Damage = damage;
            State = State.Moves;
        }

        public virtual int ToDamage(UnitParameters parameters)
        {
            Health -= 1;
            if (Health <= 0)
                IsDead = true;
            return Damage;
        }

        public void Move() => Position += Direction.ToVector() * Speed;
    }
}