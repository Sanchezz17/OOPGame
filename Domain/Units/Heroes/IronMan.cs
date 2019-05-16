using System.Windows;
using Domain.Infrastructure;
using Domain.Units.Strikes;

namespace Domain.Units.Heroes
{
    public class IronMan : BaseHero, IAttacking
    {
        public IronMan(int health, Vector position) :base(health, position, State.Idle, 15) {}
        
        public bool IsAttackAvailable() => tickСontroller.Check();
        public IStrike Attack()
        {
            return new IronManAttack(10, Position + Direction.Right.ToVector() / 2,
                Direction.Right);
        }
    }
}