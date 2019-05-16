using System.Drawing;
using Domain.Infrastructure;

namespace Domain.Units
{
    public interface IMovable
    {
        Direction Direction { get; }
        double Speed { get; }
        void Move();
    }
}