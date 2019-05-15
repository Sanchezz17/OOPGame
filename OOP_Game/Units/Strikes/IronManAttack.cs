using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Strikes
{
    class IronManAttack : Shot
    {
        public IronManAttack(int damage, Vector position, Direction direction) : base(direction, 1, position, damage) {}
    }
}
