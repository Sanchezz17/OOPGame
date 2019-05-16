using System.Windows;

namespace Domain.Units.Heroes
{
    public class CaptainAmerica : BaseHero
    {
        public CaptainAmerica(int health, Vector position) : base(health, position, State.Idle, 0) {}
    }
}