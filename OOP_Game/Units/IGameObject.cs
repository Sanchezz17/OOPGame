using System.Drawing;

namespace OOP_Game.Units
{
    public interface IGameObject
    {
        int Health { get; }
        Point Position { get; }
        State State { get; }
        bool IsDead { get;  }
    }
}