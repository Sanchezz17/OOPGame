using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units
{
    namespace OOP_Game.Units.MaleFactors
    {
        public class Thanos : IMalefactor
        {
            public int Health { get; private set; }
            public Vector Position { get; private set; }
            public State State { get;  set; }
            public bool IsDead { get; private set; }
            public double Speed { get; }
            public Direction Direction => Direction.Left;
            private TickСontroller tickСontroller;

            public Thanos(Vector position) : this(500, position) { }

            public Thanos(int health, Vector position)
            {
                Health = health;
                Position = position;
                IsDead = false;
                State = State.Moves;
                tickСontroller = new TickСontroller(15);
                Speed = 0.0125;
            }

            public bool IsAttackAvailable() => tickСontroller.Check();

            public IStrike Attack()
            {
                return new ThanosAttack(Position, Direction);
            }

            public void Move()
            {
                Position += Direction.ToVector() * Speed;
            }

            public void Trigger(IStrike strike)
            {
                var parametres = new UnitParameters(Health, Position, State, IsDead);
                Health -= strike.ToDamage(parametres);
                if (Health <= 0)
                    IsDead = true;
            }
        }
    }
}