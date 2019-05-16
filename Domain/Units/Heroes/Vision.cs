using System.Windows;

namespace Domain.Units.Heroes
{
    public class Vision : BaseHero, IGemManufacturer
    {
        public Vision(UnitParameters parametres, Vector position) : base(parametres, position, State.Produce, 150) { }

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem() => new Gem(Position, 0);
    }
}