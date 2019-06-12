using System;
using System.Windows;
using Domain.Infrastructure;
using Domain.Units.Strikes;

namespace Domain.Units.Heroes
{
    
    public class DescribeIronMan : IDescribe
    {
        public Type Type => typeof(IronMan);
        public int Price => 100;
        public UnitParameters Parameters => parameters;
        private readonly UnitParameters parameters = new UnitParameters()
            .SetHealth(3000)
            .SetDamage(10)
            .SetReload(15);
    }

    public class IronMan : BaseHero, IAttacking
    {
        public int Damage { get; private set; }
        public IronMan(UnitParameters parametres, Vector position) : base(parametres, position, State.Idle)
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