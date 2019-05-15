using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units.Heroes
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