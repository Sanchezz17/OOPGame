using System.Drawing;

namespace OOP_Game.Units.Heroes
{
    public class IronMan : IHero
    {
        public int Health { get; private set; }
        public Point Position { get; private set; }
        public State State { get; private set; }
        public bool IsDead { get; private set; }

        public IronMan(int health, Point position)
        {
            Health = health;
            Position = position;
            State = State.Idle;
        }
        
        public void Trigger(IStrike strike)
        {
            Health -= strike.ToDamage();
            if (Health == 0)
                IsDead = true;
        }

        public IStrike Attack()
        {
            return new Shot(1, Position, Infrastructure.Direction.Right);
        }
    }
}