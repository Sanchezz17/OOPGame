using System.Drawing;


namespace OOP_Game.Units
{
    public class Shot : IStrike, IMovable
    {
        public Direction Direction => Direction.Right;
        public State State => State.Moves;
        public int Health { get; private set; }
        public Point Position { get; private set; }
        
        public bool IsDead { get; private set; }

        private int damage;
        public int ToDamage()
        {
            Health -= 1;
            if (Health == 0)
                IsDead = true;
            return damage;
        }

        public Shot(int damage, Point position)
        {
            Health = 1;
            this.damage = damage;
            Position = position;
        }
        
        public void Move()
        {
            Position = Position + (Size)DirectionExtension.DirectionToPoint(this.Direction);
        }
    }
}