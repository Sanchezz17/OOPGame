using System.Windows;

namespace Domain.Units.Heroes
{
    public class CaptainAmerica : BaseHero
    {
        public CaptainAmerica(UnitParameters parametres, Vector position) : base(parametres, position, State.Idle, 0) { }
    }
}