using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.MaleFactors
{
    public class Thanos : IMalefactor
    {
        public int Health { get; private set; }
        public Point Position { get; private set; }
        public State State { get; private set; }
        public bool IsDead { get; private set; }
        public Direction Direction => Direction.Left;

        public Thanos(int health, Point position)
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
            Position += (Size)Direction.ToPoint();
        }

        public void Trigger(IStrike strike)
        {
            Health -= strike.ToDamage();
            if (Health == 0)
                IsDead = true;
        }
    }
}
