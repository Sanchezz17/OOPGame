using System.Windows;
using OOP_Game.Infrastructure;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;


namespace OOP_Game.Units
{
    public abstract class Shot : IStrike, IMovable
    {
        public Direction Direction { get; set; }
        public int Health{ get; set; }
        public Vector Position { get; set; }
        public bool IsDead { get; set; }
        public int Damage { get; set; }
        public State State { get; set; }
        public double Speed => 0.5;

        public virtual int ToDamage(UnitParameters parameters)
        {
            Health -= 1;
            if (Health <= 0)
                IsDead = true;
            return Damage;
        }

        public void Move()
        {
            Position += Direction.ToVector() * Speed;
        }
    }
}