using System.Windows;
using Domain.Infrastructure;

namespace Domain.Units
{
    public class Gem : IGameObject, IMovable
    {
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        public Direction Direction => Direction.Down;

        public double Speed { get; }

        public Gem(Vector position, double speed)
        {
            Position = position;
            State = State.Moves;
            Speed = speed;
        }
        
        public void Move() => Position += Direction.ToVector() * Speed;
    }
}