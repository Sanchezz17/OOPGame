using System;
using System.Windows;

namespace Domain.Units.Heroes
{
    public class DescribeCaptainAmerica : IDescribe
    {
        public Type Type => typeof(CaptainAmerica);
        public int Price => 50;
        public UnitParameters Parameters => parameters;
        private readonly UnitParameters parameters = new UnitParameters().SetHealth(10000);
    }

    public class CaptainAmerica : BaseHero
    {
        public CaptainAmerica(UnitParameters parametres, Vector position) : base(parametres, position, State.Idle) { }
    }
}