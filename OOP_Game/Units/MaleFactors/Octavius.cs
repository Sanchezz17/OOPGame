using System.Windows;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units
{
    public class Octavius : BaseMalefactor
    {
        public Octavius(Vector position) : this(100, position){}

        public Octavius(int health, Vector position) : base(health, position, State.Moves, 0.025, 15) {}
        
        public override IStrike Attack() => new IronManAttack(3, Position, Direction);
        
    }
}