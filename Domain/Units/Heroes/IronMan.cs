using System.Windows;
using Domain.Infrastructure;
using Domain.Units.Strikes;

namespace Domain.Units.Heroes
{
    public class IronMan : BaseHero, IAttacking
    {
        public int Damage { get; private set; }
        public IronMan(UnitParameters parametres, Vector position) : base(parametres, position, State.Idle, 15)
        {
            Damage = parametres.Damage;
        }

        public bool IsAttackAvailable() => tickСontroller.Check();
        public IStrike Attack()
        {
            return new IronManAttack(Damage, Position + Direction.Right.ToVector() / 2,
                Direction.Right);
        }
    }
}