using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public class Gem : IGameObject, ICollectable, IMovable
    {
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        public Direction Direction => Direction.Down;

        public double Speed { get; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Gem(Vector position, double speed)
        {
            Position = position;
            State = State.Moves;
            Speed = speed;
        }
        
        public void Move() => Position += Direction.ToVector() * Speed;
    }
}