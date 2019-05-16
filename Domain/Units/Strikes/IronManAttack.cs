using System.Windows;
using Domain.Infrastructure;

namespace Domain.Units.Strikes
{
    class IronManAttack : Shot
    {
        public IronManAttack(int damage, Vector position, Direction direction) : base(direction, 1, position, damage) {}
    }
}
