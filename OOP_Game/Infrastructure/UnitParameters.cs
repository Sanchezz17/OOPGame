using OOP_Game.Units;
using System.Windows;

namespace OOP_Game.Infrastructure
{
    public class UnitParameters
    {

        public int Health { get; }
        public Vector Position { get;  }
        public State State { get; }
        public bool IsDead { get;  }
        public Direction Direction { get; }

        public UnitParameters(int health, Vector position, State state, bool isDead)
        {
            Health = health;
            Position = position;
            State = state;
            IsDead = isDead;
        }
    }
}
