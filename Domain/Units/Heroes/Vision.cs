using System;
using System.Windows;

namespace Domain.Units.Heroes
{
    public class DescribeVision : IDescribe
    {
        public Type Type => typeof(Vision);
        public int Price => 50;
        public UnitParameters Parameters => new UnitParameters().SetHealth(1000).SetReload(150);
    }

    public class Vision : BaseHero, IGemManufacturer
    {
        public Vision(UnitParameters parametres, Vector position) : base(parametres, position, State.Produce) { }

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem() => new Gem(Position, 0);
    }
}