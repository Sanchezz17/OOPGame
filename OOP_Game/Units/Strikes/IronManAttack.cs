using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Strikes
{
    class IronManAttack : Shot
    {
        public IronManAttack(int damage, Vector position, Direction direction)
        {
            Health = 1;
            Damage = damage;
            Position = position;
            Direction = direction;
            State = State.Moves;
        }

    }
}
