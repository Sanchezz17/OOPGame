using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    namespace OOP_Game.Units.MaleFactors
    {
        public class Thanos : IMalefactor
        {
            public int Health { get; private set; }
            public Vector Position { get; private set; }
            public State State { get; private set; }
            public bool IsDead { get; private set; }
            public Direction Direction => Direction.Left;

            public Thanos(int health, Vector position)
            {
                Health = health;
                Position = position;
                IsDead = false;
                State = State.Moves;
            }

            public IStrike Attack()
            {
                return new Shot(10, Position, Direction);
            }

            public void Move()
            {
                Position += Direction.ToVector() * .1;
            }

            public void Trigger(IStrike strike)
            {
                Health -= strike.ToDamage();
                if (Health == 0)
                    IsDead = true;
            }
        }
    }
}