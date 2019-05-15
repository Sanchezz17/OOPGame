using System.Windows;

namespace OOP_Game.Units.Heroes
{
    public class Vision : BaseHero, IGemManufacturer
    {
        public Vision(int health, Vector position)  : base(health, position, State.Produce, 150) {}

        public bool IsAvailableGem() => tickСontroller.Check();

        public Gem GetGem() => new Gem(Position, 0);
    }
}