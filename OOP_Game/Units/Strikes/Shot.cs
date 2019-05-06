using System.Windows;
using OOP_Game.Infrastructure;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;


namespace OOP_Game.Units
{
    public class Shot : IStrike, IMovable
    {
        public Direction Direction { get; private set; }
        public State State => State.Moves;
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        
        public bool IsDead { get; private set; }

        private int damage;
        public int ToDamage()
        {
            Health -= 1;
            if (Health == 0)
                IsDead = true;
            return damage;
        }

        public Shot(int damage, Vector position, Direction direction)
        {
            Health = 1;
            this.damage = damage;
            Position = position;
            Direction = direction;
        }
        
        public void Move()
        {
            Position += Direction.ToVector() * .5;
        }
    }
}