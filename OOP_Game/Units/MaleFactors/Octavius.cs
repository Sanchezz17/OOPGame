using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units
{
    public class Octavius : IMalefactor
    {
        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get; set; }
        public bool IsDead { get; private set; }
        public Direction Direction { get; }
        public double Speed { get; }

        private TickСontroller tickСontroller;

        public Octavius(Vector position) : this(100, position){}

        public Octavius(int health, Vector position)
        {
            Health = health;
            Position = position;
            IsDead = false;
            State = State.Moves;
            tickСontroller = new TickСontroller(15);
            Speed = 0.025;
        }
        
        public void Move()
        {
            Position += Direction.ToVector() * Speed;
        }

        public IStrike Attack()
        {
            return new IronManAttack(3, Position, Direction);
        }

        public bool IsAttackAvailable() => tickСontroller.Check();

        public void Trigger(IStrike strike)
        {
            var parametres = new UnitParameters(Health, Position, State, IsDead);
            Health -= strike.ToDamage(parametres);
            if (Health <= 0)
                IsDead = true;
        }
    }
}