using System.Drawing;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public interface IMovable
    {
        Direction Direction { get; }
        double Speed { get; }
        void Move();
    }
}