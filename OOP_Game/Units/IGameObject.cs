using System.Drawing;
using System.Windows;

namespace OOP_Game.Units
{
    public interface IGameObject
    {
        int Health { get; }
        Vector Position { get; }
        State State { get; }
        bool IsDead { get;  }
    }
}