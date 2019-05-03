using System.Drawing;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public interface IMovable
    {
        Direction Direction { get; }
        void Move();
    }
}