using System.Drawing;

namespace OOP_Game.Units
{
    public interface IMovable
    {
        Direction Direction { get; }
        void Move();
    }
}